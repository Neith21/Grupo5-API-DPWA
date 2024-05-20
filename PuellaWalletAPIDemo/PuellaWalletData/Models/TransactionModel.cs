namespace PuellaWalletData.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TransactionModel
    {
        public int IdTransaction { get; set; }

        public string TransactionInfo { get; set; }

        public decimal TransactionUSD { get; set; }

        public float? TransactionBTC { get; set; }

        public int IdWallet { get; set; }

        public WalletModel? Wallet { get; set; }
    }

}
