using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("FixedWindowPolicy")]
    [ApiController]
    [SwaggerTag("Operaciones de Autenticación")]
    [ApiVersion("2.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        /// <inheritdoc />
        public CategoriesController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista la totalidad de categorias",
            Description = "Retorna un objeto generico con el resultado de la operación"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Categorias encontrado", typeof(Response<IEnumerable<CategoryDto>>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoryApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}