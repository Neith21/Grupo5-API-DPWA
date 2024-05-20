using FluentValidation;
using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Validations
{
    public class WalletValidator : AbstractValidator<WalletModel>
    {
        public WalletValidator()
        {
            RuleFor(x => x.WalletUSD)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El USD en cartera es obligatorio")
                .InclusiveBetween(0.01m, 1000000m).WithMessage("El USD en cartera debe estar entre 0.01 y 1,000,000");

            RuleFor(x => x.WalletBTC)
                .GreaterThanOrEqualTo(0).WithMessage("El BTC en cartera debe ser mayor o igual a 0")
                .When(x => x.WalletBTC.HasValue);

            RuleFor(x => x.IdUser)
                .NotEmpty().WithMessage("El ID de usuario es obligatorio")
                .Must(id => id is int).WithMessage("El ID de usuario debe ser un número entero válido");
        }
    }
}
