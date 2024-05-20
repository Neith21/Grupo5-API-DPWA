using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PuellaWalletData.Models
{
    public class WalletModel
    {
        public int IdWallet { get; set; }

        public decimal WalletUSD { get; set; }

        public float? WalletBTC { get; set; }

        public int IdUser { get; set; }

        public UserModel? User { get; set; }
    }
}
