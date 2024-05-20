using FluentValidation;
using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Validations
{
    public class TransactionValidator : AbstractValidator<TransactionModel>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.TransactionInfo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La información de la transacción es obligatoria")
                .MaximumLength(50).WithMessage("La información de la transacción no puede exceder 50 caracteres")
                .Matches(@"^[^{}<>]*$").WithMessage("La información de la transacción no puede contener { o } o < o >");

            RuleFor(x => x.TransactionUSD)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El USD de la transacción es obligatorio")
                .InclusiveBetween(0.01m, 1000000m).WithMessage("El USD de la transacción debe estar entre 0.01 y 1,000,000");

            RuleFor(x => x.TransactionBTC)
                .GreaterThanOrEqualTo(0).WithMessage("El BTC de la transacción debe ser mayor o igual a 0")
                .When(x => x.TransactionBTC.HasValue);

            RuleFor(x => x.IdWallet)
                .NotEmpty().WithMessage("El ID de cartera es obligatorio")
                .Must(id => id is int).WithMessage("El ID de cartera debe ser un número entero válido");
        }
    }
}
