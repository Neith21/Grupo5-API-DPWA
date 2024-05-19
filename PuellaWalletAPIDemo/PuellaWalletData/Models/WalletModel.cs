﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PuellaWalletData.Models
{
    public class WalletModel
    {
        [Key]
        public int IdWallet { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "USD en Cartera")]
        public decimal WalletUSD { get; set; }

        [Display(Name = "BTC en Cartera")]
        public float? WalletBTC { get; set; }

        [ForeignKey("User")]
        [Display(Name = "ID de Usuario")]
        public int IdUser { get; set; }

        public UserModel? User { get; set; }
    }
}
