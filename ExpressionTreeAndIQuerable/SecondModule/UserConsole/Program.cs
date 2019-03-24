using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomMapperLibrary;

namespace UserConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UserA a = new UserA
            {
                Age = 23,
                Name = "Maksim",
                Surname = "Harbachou",
                Adress = "123/2",
                CardNumber = 12303246,
                FullAdress = new Adress
                {
                    Prospect = "Some street",
                    HouseNumber = 123,
                    FlatNumber = 30
                }
            };

            #region Results of mapping without rules
            var mapper = MapperGenerator<UserA, UserB>.CreateMapperGenerator()
                .GenerateMapper();
            UserB b = mapper.Map(a);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.ReadLine();
            #endregion

            #region Results of mapping between user A and user B with some rules
            mapper = MapperGenerator<UserA, UserB>.CreateMapperGenerator()
                .CreateRule(sourceUser => sourceUser.Name, destUser => destUser.Surname)
                .CreateRule(sourceUser => sourceUser.Age, destUser => destUser.CardNumber)
                .GenerateMapper();
            b = mapper.Map(a);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.ReadLine();
            #endregion

            #region Results of mapping between user A and his view model. Everything is mapped by rules
            var newMapper = MapperGenerator<UserA, UserAViewModel>.CreateMapperGenerator()
                .CreateRule(sourceUser => sourceUser.Name, destUser => destUser.ViewModelName)
                .CreateRule(sourceUser => sourceUser.Surname, destUser => destUser.ViewModelSurname)
                .CreateRule(sourceUser => sourceUser.Age, destUser => destUser.ViewModelAge)
                .CreateRule(sourceUser => sourceUser.Adress, destUser => destUser.ViewModelAdress)
                .CreateRule(sourceUser => sourceUser.CardNumber, destUser => destUser.ViewModelCardNumber)
                .CreateRule(sourceUser => sourceUser.FullAdress, destUser => destUser.ViewModelFullAdress)
                .GenerateMapper();
            var aViewModel = newMapper.Map(a);
            Console.WriteLine(a);
            Console.WriteLine(aViewModel);
            Console.ReadLine();
            #endregion
        }
    }

    public class UserA
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Adress FullAdress { get; set; }
        public string Adress;
        public int CardNumber { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} age: {this.Age} | name: {this.Name} | surname: {this.Surname} | adress: {this.Adress} | full adress: {this.FullAdress} | card number: {this.CardNumber}";
        }
    }

    public class UserB : UserA
    {
        
    }

    public class Adress
    {
        public string Prospect { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }

        public override string ToString()
        {
            return $"Prospect: {this.Prospect} | House number: {this.HouseNumber} | Flat number : {this.FlatNumber}";
        }
    }

    public class UserAViewModel
    {
        public int ViewModelAge { get; set; }
        public string ViewModelName { get; set; }
        public string ViewModelSurname { get; set; }
        public Adress ViewModelFullAdress { get; set; }
        public string ViewModelAdress;
        public int ViewModelCardNumber { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} ViewModel age: {this.ViewModelAge} | ViewModel name: {this.ViewModelName} | ViewModel surname: {this.ViewModelSurname} | ViewModel adress: {this.ViewModelAdress} | ViewModel full adress: {this.ViewModelFullAdress} | ViewModel card number: {this.ViewModelCardNumber}";
        }
    }
}
