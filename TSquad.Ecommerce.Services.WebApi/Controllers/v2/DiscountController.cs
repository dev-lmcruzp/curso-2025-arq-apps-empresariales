using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.Services.WebApi.Modules.Feature;

namespace TSquad.Ecommerce.Services.WebApi.Controllers.v2
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones relacionadas con descuentos")]
    [Authorize]
    [ApiVersion("2.0")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountApplication _discountApplication;
        
        /// <inheritdoc />
        public DiscountController(IDiscountApplication discountApplication)
        {
            _discountApplication = discountApplication;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAllWithPaginationAsync")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _discountApplication.GetAllWithPaginationAsync(pageNumber, pageSize);
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountApplication.GetAllAsync(HttpContext.RequestAborted);
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        [HttpGet("{discountId:int}")]
        [RequestTimeout(PresentationConstant.MyPolicyRequestTimeout)]
        public async Task<IActionResult> Get([FromRoute] int discountId)
        {
            var response = await _discountApplication.GetAsync(discountId, HttpContext.RequestAborted);
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
        public async Task<IActionResult> Create([FromBody] DiscountDto body)
        {
            var response = await _discountApplication.InsertAsync(body);
            return response.IsSuccess 
                ? Ok(response) 
                : BadRequest(response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountId"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut("{discountId:int}")]
        public async Task<IActionResult> Update([FromRoute] int discountId, [FromBody] DiscountDto body)
        {
            if(!discountId.Equals(body.Id))
                return BadRequest();
            
            var response = await _discountApplication.UpdateAsync(body);
            return response.IsSuccess 
                ? Ok(response) 
                : BadRequest(response);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountId"></param>
        /// <returns></returns>
        [HttpDelete("{discountId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int discountId)
        {
            var response = await _discountApplication.DeleteAsync(discountId);
            return response.IsSuccess 
                ? Ok(response) 
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
