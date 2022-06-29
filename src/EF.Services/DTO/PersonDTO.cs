using System;

namespace EF.Services.DTO
{
    public class PersonDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Uf { get; set; }
        public DateTime BirthDate { get; set; }

        public PersonDTO()
        { }

        public PersonDTO(long id, string code, string name, string cpf, string uf, DateTime birthDate)
        {
            Id = id;
            Code = code;
            Name = name;
            Cpf = cpf;
            Uf = uf;
            BirthDate = birthDate;
        }
    }
}