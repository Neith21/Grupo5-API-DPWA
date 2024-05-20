using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }

        public string UserName { get; set; }

        public int UserAge { get; set; }

        public string UserEMail { get; set; }
    }
}
