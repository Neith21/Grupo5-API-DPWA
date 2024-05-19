using PuellaWalletData.Data;
using PuellaWalletData.Models;

namespace PuellaWalletData.Repositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbDataAccess _dataAccess;

        public TransactionRepository(IDbDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync()
        {
            var transactions = await _dataAccess.GetDataForeignAsync<TransactionModel, WalletModel, dynamic>(
                "dbo.spTransaction_GetAll",
                new { },
                (transaction, wallet) =>
                {
                    transaction.Wallet = wallet;
                    return transaction;
                },
                splitOn: "IdWallet"
            );

            return transactions;
        }

        public async Task<TransactionModel?> GetTransactionByIdAsync(int id)
        {
            var transactions = await _dataAccess.GetDataAsync<TransactionModel, dynamic>(
                "dbo.spTransaction_GetById",
                new { IdTransaction = id }
            );

            return transactions.FirstOrDefault();
        }

        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spTransaction_Insert",
                new { transaction.TransactionInfo, transaction.TransactionUSD, transaction.IdWallet }
            );
        }

        public async Task EditTransactionAsync(TransactionModel transaction)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spTransaction_Update",
                new { transaction.IdTransaction, transaction.TransactionInfo, transaction.TransactionUSD, transaction.IdWallet }
            );
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spTransaction_Delete",
                new { IdTransaction = id }
            );
        }
    }
}
