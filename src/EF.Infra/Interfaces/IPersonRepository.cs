using System.Collections.Generic;
using System.Threading.Tasks;
using EF.Domain.Entities;

namespace EF.Infra.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Person>> Get();
        Task<Person> GetByCode(string code);
        Task<List<Person>> GetByUf(string uf);
        Task<Person> GetExistsPerson(string code, string name, string cpf);
    }
}