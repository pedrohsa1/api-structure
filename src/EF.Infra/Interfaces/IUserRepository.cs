using EF.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF.Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> Get();
        Task<User> GetByUsername(string username);
    }
}