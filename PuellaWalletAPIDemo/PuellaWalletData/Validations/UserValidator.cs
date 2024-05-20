using FluentValidation;
using PuellaWalletData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuellaWalletData.Validations
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio")
                .MinimumLength(3).WithMessage("El nombre de usuario debe contener mínimo 3 letras")
                .MaximumLength(100).WithMessage("El nombre de usuario no puede exceder 100 caracteres")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("El nombre de usuario solo puede contener letras, números y espacios")
                .Matches(@"^[^{}<>]*$").WithMessage("El nombre de usuario no puede contener { o } o < o >");

            RuleFor(x => x.UserAge)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La edad de usuario es obligatoria")
                .InclusiveBetween(0, 150).WithMessage("La edad de usuario debe estar entre 0 y 150")
                .Must(age => age is int).WithMessage("La edad de usuario debe ser un número entero válido");

            RuleFor(x => x.UserEMail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio")
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido")
                .MaximumLength(50).WithMessage("El correo electrónico no puede exceder 50 caracteres")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("El formato del correo electrónico no es válido")
                .Matches(@"^[^{}<>]*$").WithMessage("El correo electrónico no puede contener { o } o < o >");
        }
    }
}
