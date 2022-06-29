using System;
using System.ComponentModel.DataAnnotations;

namespace EF.API.ViewModes
{
    public class UpdatePersonViewModel
    {
        [Required(ErrorMessage = "O Id não pode ser vazio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O id não pode ser menor que 1.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O código não pode ser vazio.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O nome não pode ser vazio.")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        [RegularExpression(@"^[ a-zA-Z á]*$", ErrorMessage = "O nome informado é inválido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CPF não pode ser vazio.")]
        [StringLength(14, ErrorMessage = "O CPF deve conter 14 caracteres com a máscara.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "O CPF informado é inválido. Digite o CPF com a máscara (XXX.XXX.XXX-XX).")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A UF não pode ser vazio.")]
        [StringLength(2, ErrorMessage = "A UF deve conter 2 caracteres.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "A UF informada é inválida.")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento não pode ser vazio.")]
        public string BirthDate { get; set; }
    }
}