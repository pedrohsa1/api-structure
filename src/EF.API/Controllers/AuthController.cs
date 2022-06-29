using System.Threading.Tasks;
using EF.API.Token;
using EF.API.Token.Config;
using EF.API.Utilities;
using EF.API.ViewModes;
using EF.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF.API.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserService _userService;

        public AuthController(ITokenGenerator tokenGenerator,
                              IUserService userService)
        {
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if(loginViewModel == null
                    || string.IsNullOrEmpty(loginViewModel.Username) || string.IsNullOrWhiteSpace(loginViewModel.Username)
                    || string.IsNullOrEmpty(loginViewModel.Password) || string.IsNullOrWhiteSpace(loginViewModel.Password))
                {
                    return BadRequest(ResponseUtil.UnauthorizedErrorMessage());
                }

                var user = await _userService.GetByUsername(loginViewModel.Username); 

                if (user != null)
                {
                    if(loginViewModel.Password == user.Password) 
                    {
                        AuthResult tokenUser = _tokenGenerator.GenerateJwtToken(user);

                        return Ok(new ResultViewModel
                        {
                            Message = "Usuário autenticado com sucesso!",
                            Success = true,
                            Data = new
                            {
                                Token = tokenUser.Token,
                                Success = tokenUser.Success,
                                TokenExpires = tokenUser.TokenExpires
                            }
                        });
                    }
                }

                return StatusCode(401, ResponseUtil.UnauthorizedErrorMessage());
            }
            catch
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }
    }
}