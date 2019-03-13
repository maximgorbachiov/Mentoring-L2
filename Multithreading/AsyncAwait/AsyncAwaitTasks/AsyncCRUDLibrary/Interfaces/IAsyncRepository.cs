using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncCRUDLibrary.Interfaces
{
    public interface IAsyncRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<IEnumerable<User>> ReadUserBySelectorAsync(Predicate<User> selector);
        Task<bool> UpdateUserAsync(Predicate<User> selector, User updatedUser);
        Task<bool> DeleteUserAsync(Predicate<User> selector);
    }
}
