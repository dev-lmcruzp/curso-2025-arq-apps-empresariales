using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.UseCases;

namespace TSquad.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <inheritdoc />
    // versionado por path param
    [Route("api/v{version:apiVersion}/[controller]")]
    // versionado por query y cabecera
    // [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones de Autenticación")]
    [ApiVersion("2.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        /// <inheritdoc />
        public AuthController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("SignUp")]
        [SwaggerOperation("Registra un nuevo usuario")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto body)
        {
            var response = await _authApplication.SignUpAsync(body);
            if(response.IsSuccess)
                return Ok(response);
            
            return BadRequest(response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("SignIn")]
        [SwaggerOperation("Autentica un usuario y genera un token")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInDto body)
        {
            var response = await _authApplication.SignInAsync(body);
            if (!response.IsSuccess)
                return Unauthorized(response);
            
            if(response.Data is not null)
                return Ok(response);
            
            if(response.Errors.Any())
                return BadRequest(response);
            
            return Unauthorized(response);
        }
    }
}
