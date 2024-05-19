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
        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre de Usuario")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "El nombre de usuario solo puede contener letras, números y espacios.")]
        public string UserName { get; set; }

        [Required]
        [Range(0, 150)]
        [Display(Name = "Edad de Usuario")]
        public int UserAge { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Correo Electrónico")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string UserEMail { get; set; }
    }
}
