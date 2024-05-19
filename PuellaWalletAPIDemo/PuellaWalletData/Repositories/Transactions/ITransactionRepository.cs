using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Repositories.Transactions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync();
        Task<TransactionModel?> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(TransactionModel transaction);
        Task EditTransactionAsync(TransactionModel transaction);
        Task DeleteTransactionAsync(int id);
    }
}
