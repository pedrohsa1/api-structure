using FluentValidation;
using EF.Domain.Entities;

namespace EF.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("A senha não pode ser nula.")

                .NotEmpty()
                .WithMessage("A senha não pode ser vazia.")

                .MinimumLength(6)
                .WithMessage("A senha deve ter no mínimo 6 caracteres.")

                .MaximumLength(80)
                .WithMessage("A senha deve ter no máximo 30 caracteres.");


            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage("O usuário não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O usuário não pode ser vazio.")

                .MinimumLength(10)
                .WithMessage("O usuário deve conter no mínimo 3 catacteres.")

                .MaximumLength(180)
                .WithMessage("O usuário deve conter no máximo 80 catacteres.");
        }
    }
}
