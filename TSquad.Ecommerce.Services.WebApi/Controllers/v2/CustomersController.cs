using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <inheritdoc />
    // versionado por path param
    [Route("api/v{version:apiVersion}/[controller]")]
    // versionado por query y cabecera
    // [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones relacionadas con clientes")]
    [Authorize]
    [ApiVersion("2.0")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;
        
        /// <inheritdoc />
        public CustomersController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista la totalidad de clientes",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Clientes encontrado", typeof(Response<IEnumerable<CustomerDto>>))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerApplication.GetAllAsync();
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAllWithPaginationAsync")]
        [SwaggerOperation(
            Summary = "Lista los clientes paginado",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Clientes encontrado", typeof(ResponsePagination<IEnumerable<CustomerDto>>))]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _customerApplication.GetAllWithPaginationAsync(pageNumber, pageSize);
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("byId")]
        [SwaggerOperation(
            Summary = "Obtiene un cliente en función a su ID",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente encontrado", typeof(Response<CustomerDto>))]
        public async Task<IActionResult> Get([FromQuery] string customerId)
        {
            if(string.IsNullOrEmpty(customerId))
                return BadRequest();
            
            var response = await _customerApplication.GetAsync(customerId);
            if (!response.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            
            if(response.Data is not null)
                return Ok(response);

            return NotFound(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Registra un cliente",
            Description = "Retorna un objeto generico con el resultado de la operación"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente Registrado", typeof(Response<bool>))]
        public async Task<IActionResult> Create([FromBody] CustomerDto body)
        {
            var response = await _customerApplication.InsertAsync(body);
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        [SwaggerOperation(
            Summary = "Actualiza un cliente",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente Actualizado", typeof(Response<bool>))]
        public async Task<IActionResult> Update([FromRoute] string customerId, [FromBody] CustomerDto body)
        {
            if(!customerId.Equals(body.CustomerId))
                return BadRequest();
            
            var response = await _customerApplication.UpdateAsync(body);
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        [SwaggerOperation(
            Summary = "Elimina un cliente",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente Eliminado", typeof(Response<bool>))]
        public async Task<IActionResult> Delete([FromRoute] string customerId)
        {
            if(string.IsNullOrEmpty(customerId))
                return BadRequest();
            
            var response = await _customerApplication.DeleteAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);
            
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
