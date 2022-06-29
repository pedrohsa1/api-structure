using System.Threading.Tasks;
using System.Collections.Generic;
using EF.Services.DTO;

namespace EF.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDTO> Create(PersonDTO userDTO);
        Task<PersonDTO> Update(PersonDTO userDTO);
        Task Remove(long id);
        Task<PersonDTO> Get(long id);
        Task<PersonDTO> GetCode(string code);
        Task<List<PersonDTO>> GetUf(string uf);
        Task<List<PersonDTO>> Get();
    }
}