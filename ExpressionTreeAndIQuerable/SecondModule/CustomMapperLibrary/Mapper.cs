using System;

namespace CustomMapperLibrary
{
    public class Mapper<TSource, TDestination>
    {
        private Func<TSource, TDestination> mapFunction;

        public Mapper(Func<TSource, TDestination> mapFunction)
        {
            this.mapFunction = mapFunction;
        }

        public TDestination Map(TSource source)
        {
            return this.mapFunction(source);
        }
    }
}
