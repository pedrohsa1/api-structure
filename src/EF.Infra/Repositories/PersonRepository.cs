using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Domain.Entities;
using EF.Infra.Context;
using EF.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF.Infra.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly EFContext _context;

        public PersonRepository(EFContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Person>> Get()
        {
            return await _context.Persons
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public override async Task<Person> Get(long id)
        {
            var user = await _context.Persons
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<Person> GetByCode(string code)
        {
            var person = await _context.Persons
                       .Where
                       (
                            x =>
                                x.Code.Trim() == code.Trim()
                        )
                        .AsNoTracking()
                        .ToListAsync();

            return person.FirstOrDefault();
        }

        public async Task<List<Person>> GetByUf(string uf)
        {
            return await _context.Persons
               .Where
               (
                    x =>
                        x.Uf.Trim() == uf.Trim()
                )
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Person> GetExistsPerson(string code, string name, string cpf)
        {
            var person = await _context.Persons
               .Where
               (
                    x =>
                        x.Code.Trim() == code.Trim() ||
                        x.Name.ToLower().Trim() == name.ToLower().Trim() ||
                        x.Cpf.Trim() == cpf.Trim()
                )
                .AsNoTracking()
                .ToListAsync();

            return person.FirstOrDefault();
        }
    }
}