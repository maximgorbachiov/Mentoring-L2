using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomMapperLibrary
{
    public class MapperGenerator<TSource, TDestination>
    {
        // A set of rules which should be applied if they exist
        // Show which property from source object should be applied to some destination property
        private List<Rule> additionalRules = new List<Rule>();

        private class Rule
        {
            public string SourcePropertyName { get; set; }
            public string DestinationPropertyName { get; set; }
        }

        public static MapperGenerator<TSource, TDestination> CreateMapperGenerator()
        {
            return new MapperGenerator<TSource, TDestination>();
        }

        /// <summary>
        /// Create rule of mapping source model to destination model
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="sourceRule"></param>
        /// <param name="destinationRule"></param>
        /// <returns></returns>
        public MapperGenerator<TSource, TDestination> CreateRule<TMember>(Expression<Func<TSource, TMember>> sourceRule, Expression<Func<TDestination, TMember>> destinationRule)
        {
            var sourceMemberExpression = sourceRule.Body as MemberExpression;
            var destinationMemberExpression = destinationRule.Body as MemberExpression;

            if (sourceMemberExpression != null && destinationMemberExpression != null)
            {
                this.additionalRules.Add(new Rule
                {
                    SourcePropertyName = sourceMemberExpression.Member.Name,
                    DestinationPropertyName = destinationMemberExpression.Member.Name
                });
            }

            return this;
        }

        /// <summary>
        /// Generate specified mapper
        /// </summary>
        /// <returns></returns>
        public Mapper<TSource, TDestination> GenerateMapper()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var destinationParam = Expression.Parameter(typeof(TDestination));
            List<Expression> expressions = new List<Expression>();

            // Getting property and field members of source and destination types
            MemberInfo[][] sourceMembers = this.GetPropertyAndFields<TSource>();
            MemberInfo[][] destinationMembers = this.GetPropertyAndFields<TDestination>();

            // create expression which initialize destination parameter (e.g. destination = new TDestination())
            expressions.Add(Expression.Assign(destinationParam, Expression.New(typeof(TDestination))));

            // go by every property and field in source type
            foreach (MemberInfo[] members in sourceMembers)
            {
                foreach (MemberInfo sourceMember in members)
                {
                    // searching for rule where source type member exist
                    var rule = additionalRules.FirstOrDefault(additionalRule => additionalRule.SourcePropertyName == sourceMember.Name);
                    // if we found rule we should apply it
                    if (rule != null)
                    {
                        // seaching for destination member with name as in rule
                        var destinationMember = this.GetDestMember(destinationMembers, rule.DestinationPropertyName);
                        // create assign expression for destination and source properties
                        // (e.g. destination.member = source.anotherMember)
                        expressions.Add(this.CreateAssignStatement(sourceParam, destinationParam, sourceMember, destinationMember));
                    }
                    // if we didn't find rule to apply we should map properties by their name as default
                    // (e.g. destination.member = source.member)
                    else
                    {
                        // if we have rules which contain destination property, so we should not apply default mapping for this property,
                        // because it should be setted by the rule (e.g. if we have {userA => user.Name, userB => user.Surname}
                        // we should not apply userA.Surname to the userB.Surname because we have a rule for it)
                        var usedRule = additionalRules.FirstOrDefault(additionalRule => additionalRule.DestinationPropertyName == sourceMember.Name);
                        // if we have rule we should skip this iteration
                        if (usedRule != null)
                        {
                            continue;
                        }

                        var destinationMember = this.GetDestMember(destinationMembers, sourceMember.Name);
                        // if destination type has no such property as in source type we should not apply this source type member to any property
                        // so we should skip this iteration
                        if (destinationMember == null)
                        {
                            continue;
                        }

                        // if destination and source types properties have the same named properties of fields but they are of different types
                        // we throw an Exception to warn user about it
                        if (!this.ValidateMemberMapping(sourceMember, destinationMember))
                        {
                            throw new Exception($"Type {nameof(TSource)} can not be mapped on {nameof(TDestination)} as they have different types for the same named properties");
                        }

                        // create assign statement
                        expressions.Add(this.CreateAssignStatement(sourceParam, destinationParam, sourceMember, destinationMember));
                    }
                }
            }
            // the last expression of block statement returns by this block
            expressions.Add(destinationParam);
            var block = Expression.Block(new ParameterExpression[] { destinationParam }, expressions);
            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(block, sourceParam);
            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        /// <summary>
        /// Create assign statement between destination parameter and source
        /// </summary>
        /// <param name="sourceParam"></param>
        /// <param name="destParam"></param>
        /// <param name="sourceMember"></param>
        /// <param name="destMember"></param>
        /// <returns></returns>
        private BinaryExpression CreateAssignStatement(ParameterExpression sourceParam, ParameterExpression destParam, MemberInfo sourceMember, MemberInfo destMember)
        {
            return Expression.Assign(
                Expression.MakeMemberAccess(destParam, destMember),
                Expression.MakeMemberAccess(sourceParam, sourceMember));
        }

        // I have no ideas how to make it easier and smaller
        private bool ValidateMemberMapping(MemberInfo sourceMember, MemberInfo destMember)
        {
            if (sourceMember.MemberType is MemberTypes.Property)
            {
                PropertyInfo sourceInfo = (PropertyInfo)sourceMember;
                if (destMember.MemberType is MemberTypes.Property)
                {
                    PropertyInfo destInfo = (PropertyInfo)destMember;
                    if (sourceInfo.PropertyType != destInfo.PropertyType)
                    {
                        return false;
                    }
                }
                else
                {
                    FieldInfo destInfo = (FieldInfo)destMember;
                    if (sourceInfo.PropertyType != destInfo.FieldType)
                    {
                        return false;
                    }
                }
            }
            else
            {
                FieldInfo sourceInfo = (FieldInfo)sourceMember;
                if (destMember.MemberType is MemberTypes.Property)
                {
                    PropertyInfo destInfo = (PropertyInfo)destMember;
                    if (sourceInfo.FieldType != destInfo.PropertyType)
                    {
                        return false;
                    }
                }
                else
                {
                    FieldInfo destInfo = (FieldInfo)destMember;
                    if (sourceInfo.FieldType != destInfo.FieldType)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Get all public properties and fields of type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private MemberInfo[][] GetPropertyAndFields<T>()
        {
            MemberInfo[][] members = new MemberInfo[2][];
            members[0] = typeof(T).GetProperties();
            members[1] = typeof(T).GetFields();

            return members;
        }

        /// <summary>
        /// Searching for destination member with given source member name
        /// </summary>
        /// <param name="destMembers"></param>
        /// <param name="sourceMemberName"></param>
        /// <returns></returns>
        private MemberInfo GetDestMember(MemberInfo[][] destMembers, string sourceMemberName)
        {
            var destinationMember = destMembers[0]
                .FirstOrDefault(dstMember => dstMember.Name == sourceMemberName);

            if (destinationMember == null)
            {
                destinationMember = destMembers[1].FirstOrDefault(dstMember => dstMember.Name == sourceMemberName);
            }

            return destinationMember;
        }
    }
}
