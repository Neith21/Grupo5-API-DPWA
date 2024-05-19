using PuellaWalletData.Data;
using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Repositories.Wallets
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IDbDataAccess _dataAccess;

        public WalletRepository(IDbDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<WalletModel>> GetAllWalletsAsync()
        {
            return await _dataAccess.GetDataAsync<WalletModel, dynamic>(
            "dbo.spWallet_GetAll",
            new { }
            );
        }

        public async Task<WalletModel?> GetWalletByIdAsync(int id)
        {
            var wallets = await _dataAccess.GetDataAsync<WalletModel, dynamic>(
                "dbo.spWallets_GetById",
                new { IdWallet = id }
            );

            return wallets.FirstOrDefault();
        }

        public async Task AddWalletAsync(WalletModel wallet)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spWallets_Insert",
                new { wallet.WalletUSD, wallet.WalletBTC, wallet.IdUser }
            );
        }

        public async Task EditWalletAsync(WalletModel wallet)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spWallets_Update",
                new { wallet.IdWallet, wallet.WalletUSD, wallet.WalletBTC, wallet.IdUser }
            );
        }

        public async Task DeleteWalletAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spWallets_Delete",
                new { IdWallet = id }
            );
        }

        private async Task<UserModel?> GetUserByIdAsync(int id)
        {
            var users = await _dataAccess.GetDataAsync<UserModel, dynamic>(
                "dbo.spUsers_GetById",
                new { IdUser = id }
            );

            return users.FirstOrDefault();
        }
    }
}
