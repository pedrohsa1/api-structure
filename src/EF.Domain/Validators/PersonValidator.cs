using FluentValidation;
using EF.Domain.Entities;

namespace EF.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A Entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A Entidade não pode ser nula.");

            RuleFor(x => x.Code)
                .NotNull()
                .WithMessage("O Codigo não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O Codigo não pode ser vazio.");

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("O Nome não pode ser nulo.")

               .NotEmpty()
               .WithMessage("O Nome não pode ser vazio.")

               .MinimumLength(3)
               .WithMessage("O Nome deve conter no mínimo 3 catacteres.")

               .MaximumLength(80)
               .WithMessage("O Nome deve conter no máximo 80 catacteres.")

               .Matches(@"^[ a-zA-Z á]*$")
               .WithMessage("O Nome deve conter apenas letras.");

            RuleFor(x => x.Cpf)
                .NotNull()
                .WithMessage("O CPF não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O CPF não pode ser vazio.")

               .Length(14)
               .WithMessage("O CPF deve conter 14 catacteres com a máscara.")

               .Matches(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$")
               .WithMessage("O CPF está no formato correto.");

            RuleFor(x => x.Uf)
               .NotNull()
               .WithMessage("A UF não pode ser nulo.")

               .NotEmpty()
               .WithMessage("A UF não pode ser vazio.")

               .Length(2)
               .WithMessage("A UF deve conter 2 catacteres.")

               .Matches("^[a-zA-Z]+$")
               .WithMessage("A UF deve conter apenas letras.");

            RuleFor(x => x.BirthDate)
               .NotEmpty()
               .WithMessage("A Data de Nascimento não pode ser vazia.")

               .NotNull()
               .WithMessage("A Data de Nascimento não pode ser nula.");
        }
    }
}
