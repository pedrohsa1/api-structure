using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EF.Core.Exceptions;
using EF.Domain.Entities;
using EF.Infra.Interfaces;
using EF.Services.DTO;
using EF.Services.Interfaces;

namespace EF.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByUsername(userDTO.Username);

            if (userExists != null)
                throw new DomainException("Já existe um usuário cadastrado com esse username.");

            var user = _mapper.Map<User>(userDTO);

            user.Validate();
            user.ChangePassword(user.Password);

            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExists = await _userRepository.Get(userDTO.Id);

            if (userExists == null)
                throw new DomainException("Não existe nenhum usuário com o id informado.");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(user.Password);

            var userUpdated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }

        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserDTO> Get(long id)
        {
            var user = await _userRepository.Get(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> Get()
        {
            var users = await _userRepository.Get();

            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByUsername(string username)
        {
            var user = await _userRepository.GetByUsername(username);

            return _mapper.Map<UserDTO>(user);
        }
    }
}