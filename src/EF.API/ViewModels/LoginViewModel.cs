using System.ComponentModel.DataAnnotations;

namespace EF.API.ViewModes
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O usuário não pode vazio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha não pode vazio.")]
        public string Password { get; set; }
    }
}