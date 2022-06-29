using EF.API.Token.Config;
using EF.Services.DTO;

namespace EF.API.Token
{
    public interface ITokenGenerator
    {
        AuthResult GenerateJwtToken(UserDTO userDTO);
    }
}