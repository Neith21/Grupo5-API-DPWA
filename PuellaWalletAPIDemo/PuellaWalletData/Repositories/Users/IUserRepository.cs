using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Repositories.Users
{
    public interface IUserRepository
    {
        Task AddUserAsync(UserModel user);
        Task DeleteUserAsync(int id);
        Task EditUserAsync(UserModel user);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetUserByIdAsync(int id);
    }
}
