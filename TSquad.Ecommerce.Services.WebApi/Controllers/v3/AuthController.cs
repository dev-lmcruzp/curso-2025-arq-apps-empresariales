using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSquad.Ecommerce.Application.UseCases.Users.Commands;
using TSquad.Ecommerce.Application.UseCases.Users.Queries;

namespace TSquad.Ecommerce.Services.WebApi.Controllers.v3
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones de Autenticación")]
    [ApiVersion("3.0")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("SignUp")]
        [SwaggerOperation("Registra un nuevo usuario")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand command)
        {
            var response = await _mediator.Send(command);
            if(response.IsSuccess)
                return Ok(response);
            
            return BadRequest(response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("SignIn")]
        [SwaggerOperation("Autentica un usuario y genera un token")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInQuery command)
        {
            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
                return Unauthorized(response);
            
            if(response.Data is not null)
                return Ok(response);
            
            return Unauthorized(response);
        }
    }
}
