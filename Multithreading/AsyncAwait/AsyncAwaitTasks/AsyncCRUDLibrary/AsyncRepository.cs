using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncCRUDLibrary.Interfaces;

namespace AsyncCRUDLibrary
{
    public class AsyncRepository : IAsyncRepository
    {
        private List<User> users = new List<User>();

        public AsyncRepository()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            string baseName = "Maksim";
            string baseSurname = "Harbachou";

            for (int i = 0; i < 100; i++)
            {
                this.users.Add(new User
                {
                    Name = baseName + i,
                    Surname = baseSurname + i,
                    Age = i
                });
            }
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Task.Factory.StartNew(() =>
            {
                bool result = false;

                Predicate<User> selector = (u) => u.Name == user.Name && u.Surname == user.Surname && u.Age == user.Age;
                User existedUser = this.users.FirstOrDefault((u) => selector(u));

                if (existedUser == null)
                {
                    this.users.Add(user.Clone() as User);
                    result = true;
                }
                return result;
            });
        }

        public async Task<IEnumerable<User>> ReadUserBySelectorAsync(Predicate<User> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return await Task.Factory.StartNew(() =>
            {
                return this.users
                    .Where(user => selector(user))
                    .Select(user => user.Clone() as User);
            });
        }

        public async Task<bool> UpdateUserAsync(Predicate<User> selector, User updatedUser)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            if (updatedUser == null)
            {
                throw new ArgumentNullException(nameof(updatedUser));
            }

            return await Task.Factory.StartNew(() =>
            {
                bool result = false;

                User userToUpdated = this.users.FirstOrDefault(user => selector(user));
                if (userToUpdated != null)
                {
                    User updatedUserClone = updatedUser.Clone() as User;
                    userToUpdated.Name = updatedUser.Name;
                    userToUpdated.Surname = updatedUser.Surname;
                    userToUpdated.Age = updatedUser.Age;
                    result = true;
                }
                return result;
            });
        }

        public async Task<bool> DeleteUserAsync(Predicate<User> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return await Task.Factory.StartNew(() =>
            {
                bool result = false;

                User userToDelete = this.users.FirstOrDefault(user => selector(user));
                if (userToDelete != null)
                {
                    this.users.Remove(userToDelete);
                    result = true;
                }
                return result;
            });
        }
    }
}
