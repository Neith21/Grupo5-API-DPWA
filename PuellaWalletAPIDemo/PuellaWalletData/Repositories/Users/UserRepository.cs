using PuellaWalletData.Data;
using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbDataAccess _dataAccess;

        public UserRepository(IDbDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spUser_Insert",
                new { user.UserName, user.UserAge, user.UserEMail }
            );
        }

        public async Task DeleteUserAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spUser_Delete",
                new { IdUser = id }
            );
        }

        public async Task EditUserAsync(UserModel user)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spUser_Update",
                new { user.IdUser, user.UserName, user.UserAge, user.UserEMail }
            );
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _dataAccess.GetDataAsync<UserModel, dynamic>(
            "dbo.spUser_GetAll",
            new { }
            );
        }

        public async Task<UserModel?> GetUserByIdAsync(int id)
        {
            var users = await _dataAccess.GetDataAsync<UserModel, dynamic>(
                "dbo.spUser_GetById",
                new { IdUser = id }
            );

            return users.FirstOrDefault();
        }
    }
}
