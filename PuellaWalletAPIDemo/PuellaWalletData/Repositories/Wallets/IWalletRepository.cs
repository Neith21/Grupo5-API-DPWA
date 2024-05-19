using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Repositories.Wallets
{
    public interface IWalletRepository
    {
        Task AddWalletAsync(WalletModel wallet);
        Task DeleteWalletAsync(int id);
        Task EditWalletAsync(WalletModel wallet);
        Task<IEnumerable<WalletModel>> GetAllWalletsAsync();
        Task<WalletModel?> GetWalletByIdAsync(int id);
    }
}
