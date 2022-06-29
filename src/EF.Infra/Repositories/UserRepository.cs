using EF.Domain.Entities;
using EF.Infra.Context;
using EF.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EFContext _context;

        public UserRepository(EFContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _context.Users
                       .Where
                       (
                            x =>
                                x.Username.ToLower().Trim() == username.ToLower().Trim()
                       )
                        .AsNoTracking()
                        .ToListAsync();

            return user.FirstOrDefault();
        }
    }
}