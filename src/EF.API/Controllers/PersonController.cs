using System;
using System.Threading.Tasks;
using EF.API.ViewModes;
using EF.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using EF.Services.Interfaces;
using AutoMapper;
using EF.Services.DTO;
using EF.API.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace EF.API.Controllers
{

    [ApiController]
    public class PersonController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public PersonController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        [HttpPost]
        [Route("/api/v1/person/create")]
        public async Task<IActionResult> Create([FromBody] CreatePersonViewModel personViewModel)
        {
            try
            {
                var personDTO = _mapper.Map<PersonDTO>(personViewModel);

                var personCreated = await _personService.Create(personDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Pessoa criada com sucesso!",
                    Success = true,
                    Data = personCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }

        [HttpPut]
        [Route("/api/v1/person/update")]
        public async Task<IActionResult> Update([FromBody] UpdatePersonViewModel personViewModel)
        {
            try
            {
                var personDTO = _mapper.Map<PersonDTO>(personViewModel);

                var personUpdated = await _personService.Update(personDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Pessoa atualizada com sucesso!",
                    Success = true,
                    Data = personUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        [Route("/api/v1/person/remove/{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                await _personService.Remove(id);

                return Ok(new ResultViewModel
                {
                    Message = "Pessoa removida com sucesso!",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/person/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var person = await _personService.Get(id);

                if (person == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhuma pessoa foi encontrado com o ID informado.",
                        Success = true,
                        Data = person
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Pessoa encontrado com sucesso!",
                    Success = true,
                    Data = person
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/person/code/{code}")]
        public async Task<IActionResult> GetCode(string code)
        {
            try
            {
                var persons = await _personService.GetCode(code);
                var message = persons == null ? $"Nenhum registro de pessoa encontrado com o código {code}." : "Pessoa(s) encontrada com sucesso!";

                return Ok(new ResultViewModel
                {
                    Message = message,
                    Success = true,
                    Data = persons
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/person/uf/{uf}")]
        public async Task<IActionResult> GetUf(string uf)
        {
            try
            {
                var persons = await _personService.GetUf(uf);
                var message = persons == null ? $"Nenhum registro de pessoa encontrado com a UF {uf}." : "Pessoa(s) encontrada com sucesso!";

                return Ok(new ResultViewModel
                {
                    Message = message,
                    Success = true,
                    Data = persons
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }


        [HttpGet]
        [Route("/api/v1/person/get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var persons = await _personService.Get();
                var message = persons == null ? $"Nenhum registro de pessoa encontrado." : "Pessoa(s) encontrada com sucesso!";

                return Ok(new ResultViewModel
                {
                    Message = "Pessoa(s) encontrada com sucesso!",
                    Success = true,
                    Data = persons
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseUtil.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseUtil.ApplicationErrorMessage());
            }
        }
    }
}