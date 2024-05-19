namespace PuellaWalletData.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TransactionModel
    {
        [Key]
        public int IdTransaction { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Información de la Transacción")]
        public string TransactionInfo { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "USD de la Transacción")]
        public decimal TransactionUSD { get; set; }

        [Display(Name = "BTC de la Transacción")]
        public float? TransactionBTC { get; set; }

        [ForeignKey("Wallet")]
        [Display(Name = "ID de Cartera")]
        public int IdWallet { get; set; }

        public WalletModel? Wallet { get; set; }
    }

}
