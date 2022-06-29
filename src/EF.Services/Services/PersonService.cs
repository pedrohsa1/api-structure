using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using EF.Core.Exceptions;
using EF.Domain.Entities;
using EF.Infra.Interfaces;
using EF.Services.DTO;
using EF.Services.Interfaces;

namespace EF.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public PersonService(
            IMapper mapper,
            IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<PersonDTO> Create(PersonDTO personDTO)
        {
            var personExists = await _personRepository.GetExistsPerson(personDTO.Code, personDTO.Name, personDTO.Cpf);

            if (personExists != null)
                throw new DomainException("Já existe uma pessoa cadastrada com essas informações.");

            var person = _mapper.Map<Person>(personDTO);
            person.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(personDTO.Name.ToLower());
            person.Code = personDTO.Code.ToUpper();
            person.Uf = personDTO.Uf.ToUpper();
            person.Validate();

            var personCreated = await _personRepository.Create(person);

            return _mapper.Map<PersonDTO>(personCreated);
        }

        public async Task<PersonDTO> Update(PersonDTO personDTO)
        {
            var personExists = await _personRepository.Get(personDTO.Id);

            if (personExists == null)
                throw new DomainException("Não existe nenhuma pessoa com o id informado!");

            var person = _mapper.Map<Person>(personDTO);
            person.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(personDTO.Name.ToLower());
            person.Code = personDTO.Code.ToUpper();
            person.Uf = personDTO.Uf.ToUpper();
            person.Validate();

            var personUpdated = await _personRepository.Update(person);

            return _mapper.Map<PersonDTO>(personUpdated);
        }

        public async Task Remove(long id)
        {
            await _personRepository.Remove(id);
        }

        public async Task<PersonDTO> Get(long id)
        {
            var person = await _personRepository.Get(id);

            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<PersonDTO> GetCode(string code)
        {
            var person = await _personRepository.GetByCode(code.ToUpper());

            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<List<PersonDTO>> GetUf(string uf)
        {
            var persons = await _personRepository.GetByUf(uf.ToUpper());

            return _mapper.Map<List<PersonDTO>>(persons);
        }

        public async Task<List<PersonDTO>> Get()
        {
            var persons = await _personRepository.Get();

            return _mapper.Map<List<PersonDTO>>(persons);
        }
    }
}