using EF.Core.Exceptions;
using EF.Domain.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.Domain.Entities{

    public class User: Base{

        [Required]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}

        public User(){}

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            _errors = new List<string>();

            Validate();
        }

        public void ChangePassword(string password)
        {
            Password = password;
            Validate();
        }

        public void ChangeUsername(string username)
        {
            Username = username;
            Validate();
        }

        public override bool Validate()
        {
            var validator = new UserValidator();
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