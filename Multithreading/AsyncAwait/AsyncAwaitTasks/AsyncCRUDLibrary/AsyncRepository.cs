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
        private object lockObject = new object();

        public AsyncRepository()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            string baseName = "Maksim";
            string baseSurname = "Harbachou";

            for (int i = 0; i < 10; i++)
            {
                this.users.Add(new User
                {
                    Name = baseName + i,
                    Surname = baseSurname + i,
                    Age = i
                });
            }
        }

        public async Task CreateUserAsync(User user, Action<bool> asyncCallback)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (asyncCallback == null)
            {
                throw new ArgumentNullException(nameof(asyncCallback));
            }

            await Task.Factory.StartNew(() =>
            {
                bool result = false;

                lock (lockObject)
                {
                    Predicate<User> selector = (u) => u.Name == user.Name && u.Surname == user.Surname && u.Age == user.Age;
                    User existedUser = this.users.FirstOrDefault((u) => selector(u));

                    if (existedUser == null)
                    {
                        this.users.Add(user.Clone() as User);
                        result = true;
                    }
                }
                return result;
            }).ContinueWith(creationTask => asyncCallback(creationTask.Result));
        }

        public async Task ReadUserBySelectorAsync(Predicate<User> selector, Action<List<User>> asyncCallback)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            if (asyncCallback == null)
            {
                throw new ArgumentNullException(nameof(asyncCallback));
            }

            await Task.Factory.StartNew(() =>
            {
                List<User> readedUsers = new List<User>();

                lock (lockObject)
                {
                    readedUsers.AddRange(this.users
                        .Where(user => selector(user))
                        .Select(user => user.Clone() as User));
                }
                return readedUsers;
            }).ContinueWith(readTask => asyncCallback(readTask.Result));
        }

        public async Task UpdateUserAsync(Predicate<User> selector, User updatedUser, Action<bool> asyncCallback)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            if (updatedUser == null)
            {
                throw new ArgumentNullException(nameof(updatedUser));
            }
            if (asyncCallback == null)
            {
                throw new ArgumentNullException(nameof(asyncCallback));
            }

            await Task.Factory.StartNew(() =>
            {
                bool result = false;

                lock (lockObject)
                {
                    User userToUpdated = this.users.FirstOrDefault(user => selector(user));

                    if (userToUpdated != null)
                    {
                        userToUpdated = updatedUser.Clone() as User;
                        result = true;
                    }
                }
                return result;
            }).ContinueWith(updateTask => asyncCallback(updateTask.Result));
        }

        public async Task DeleteUserAsync(Predicate<User> selector, Action<bool> asyncCallback)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            if (asyncCallback == null)
            {
                throw new ArgumentNullException(nameof(asyncCallback));
            }

            await Task.Factory.StartNew(() =>
            {
                bool result = false;

                lock (lockObject)
                {
                    User userToDelete = this.users.FirstOrDefault(user => selector(user));

                    if (userToDelete != null)
                    {
                        this.users.Remove(userToDelete);
                        result = true;
                    }
                }
                return result;
            }).ContinueWith(updateTask => asyncCallback(updateTask.Result));
        }
    }
}
