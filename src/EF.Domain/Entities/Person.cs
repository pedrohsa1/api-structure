using EF.Core.Exceptions;
using EF.Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.Domain.Entities{

    public class Person: Base{

        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Uf { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public Person(){}

        public Person(string code, string name, string cpf, string uf, string password, DateTime birthDate)
        {
            Code = code;
            Name = name;
            Cpf = cpf;
            Uf = uf;
            BirthDate = birthDate;
            _errors = new List<string>();

            Validate();
        }
        public void ChangeCode(string code)
        {
            Code = code;
            Validate();
        }
        public void ChangeName(string name)
        {
            Name = name;
            Validate();
        }

        public void ChangeCpf(string cpf)
        {
            Cpf = cpf;
            Validate();
        }

        public void ChangeUf(string uf)
        {
            Uf = uf;
            Validate();
        }

        public void ChangeBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
            Validate();
        }

        public override bool Validate()
        {
            var validator = new PersonValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach(var error in validation.Errors) 
                    _errors.Add(error.ErrorMessage);

                throw new DomainException("Campo(s) inválido(s)", _errors);
            }

            return true;
        }

    }
}