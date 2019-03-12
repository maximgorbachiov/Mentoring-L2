using System;
using AsyncCalculator;
using AsyncCRUDLibrary;
using AsyncCRUDLibrary.Interfaces;

namespace UserUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            #region First task
            Console.WriteLine("Task1 started");
            AsyncCalculatorClass.UserProcessStart();
            Console.WriteLine("Task1 ended");
            Console.ReadLine();
            #endregion
            */

            #region Fourth task
            Console.WriteLine("Task4 started");
            IAsyncRepository asyncRepository = new AsyncRepository();

            User createdUser = CreateUser();
            Console.WriteLine("Create operation started");
            asyncRepository.CreateUserAsync(createdUser, result =>
            {
                if (result)
                {
                    Console.WriteLine($"User {createdUser.Name} {createdUser.Surname} {createdUser.Age} was added successfully");
                }
                else
                {
                    Console.WriteLine($"User {createdUser.Name} {createdUser.Surname} {createdUser.Age} wasn't added to the DB");
                }
                Console.WriteLine("Create operation ended");
            });

            Console.WriteLine("Read operation started");
            asyncRepository.ReadUserBySelectorAsync(user => 
            {
                return user.Age > 5 && user.Age < 10;
            }, users =>
            {
                if (users != null)
                {
                    foreach (var readedUser in users)
                    {
                        Console.WriteLine($"User {readedUser.Name} {readedUser.Surname} {readedUser.Age} was readed");
                    }
                }
                Console.WriteLine("Read operation ended");
            });

            User updatedUser = CreateUpdatedUser();
            Console.WriteLine("Update operation started");
            asyncRepository.UpdateUserAsync(user => user.Name == "Maksim", updatedUser, result =>
            {
                if (result)
                {
                    Console.WriteLine($"User {createdUser.Name} {createdUser.Surname} {createdUser.Age} was successfully updated to {updatedUser.Name} {updatedUser.Surname} {updatedUser.Age}");
                }
                else
                {
                    Console.WriteLine($"User {createdUser.Name} {createdUser.Surname} {createdUser.Age} wasn't updated");
                }
                Console.WriteLine("Update operation ended");
            });

            Console.WriteLine("Delete operation started");
            asyncRepository.DeleteUserAsync(user => user.Name == "Maksim1", result =>
            {
                if (result)
                {
                    Console.WriteLine($"User with name Maksim1 was successfully deleted from DB");
                }
                else
                {
                    Console.WriteLine($"User with name Maksim1 wasn't deleted from DB");
                }
                Console.WriteLine("Delete operation ended");
            });

            Console.WriteLine("Task4 ended");
            Console.ReadLine();
            #endregion
        }

        private static User CreateUser()
        {
            return new User
            {
                Name = "Maksim",
                Surname = "Harbachou",
                Age = 23
            };
        }

        private static User CreateUpdatedUser()
        {
            return new User
            {
                Name = "Maksim23",
                Surname = "Harbachou",
                Age = 24
            };
        }
    }
}
