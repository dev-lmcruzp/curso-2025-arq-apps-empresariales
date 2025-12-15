using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using TSquad.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;
using TSquad.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;
using TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery;
using TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Services.WebApi.Controllers.v3
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("3.0")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllCustomerQuery());
            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
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
        [SwaggerResponse(StatusCodes.Status200OK, "Clientes encontrado",
            typeof(ResponsePagination<IEnumerable<CustomerDto>>))]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _mediator.Send(new GetAllWithPaginationCustomerQuery(pageNumber, pageSize));
            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
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
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await _mediator.Send(new GetCustomerQuery(customerId));
            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Registra un cliente",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente Registrado", typeof(Response<bool>))]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        [SwaggerOperation(
            Summary = "Actualiza un cliente",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Cliente Actualizado", typeof(Response<bool>))]
        public async Task<IActionResult> Update([FromRoute] string customerId, [FromBody] UpdateCustomerCommand command)
        {
            if (!customerId.Equals(command.CustomerId))
                return BadRequest();

            var response = await _mediator.Send(command);
            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
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
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await _mediator.Send(new DeleteCustomerCommand(customerId));
            return response.IsSuccess
                ? NoContent()
                : NotFound(response);
        }
    }
}