using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncCRUDLibrary.Interfaces
{
    public interface IAsyncRepository
    {
        Task CreateUserAsync(User user, Action<bool> asyncCallback);
        Task ReadUserBySelectorAsync(Predicate<User> selector, Action<List<User>> asyncCallback);
        Task UpdateUserAsync(Predicate<User> selector, User updatedUser, Action<bool> asyncCallback);
        Task DeleteUserAsync(Predicate<User> selector, Action<bool> asyncCallback);
    }
}
