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
            var wallets = await _dataAccess.GetDataForeignAsync<WalletModel, UserModel, dynamic>(
            "dbo.spWallet_GetAll",
            new { },
            (wallet, user) =>
            {
                wallet.User = user;
                return wallet;
            },
            splitOn: "IdUser"
        );

            return wallets;
        }

        public async Task<WalletModel?> GetWalletByIdAsync(int id)
        {
            var wallets = await _dataAccess.GetDataAsync<WalletModel, dynamic>(
                "dbo.spWallet_GetById",
                new { IdWallet = id }
            );

            return wallets.FirstOrDefault();
        }

        public async Task AddWalletAsync(WalletModel wallet)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spWallet_Insert",
                new { wallet.WalletUSD, wallet.IdUser }
            );
        }

        public async Task EditWalletAsync(WalletModel wallet)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spWallet_Update",
                new { wallet.IdWallet, wallet.WalletUSD, wallet.IdUser }
            );
        }

        public async Task DeleteWalletAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spWallet_Delete",
                new { IdWallet = id }
            );
        }
    }
}
