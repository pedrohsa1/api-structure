using System.Threading.Tasks;
using System.Collections.Generic;
using EF.Services.DTO;

namespace EF.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task Remove(long id);
        Task<UserDTO> Get(long id);
        Task<List<UserDTO>> Get();
        Task<UserDTO> GetByUsername(string username);
    }
}