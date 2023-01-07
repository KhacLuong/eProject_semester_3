using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;


namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        // GET: api/RateModelGets
        [HttpGet]
        public async Task<ActionResult<Object>> GetRatesByProductId(int productId, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _rateService.GetRatesByProductIdAsync(productId, pageSize, pageIndex);

                return result == null
                    ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                    : Ok(new MyServiceResponse<object>(result));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        // GET: api/RateModelGets
        [HttpGet("GetRateAndComment")]
        public async Task<ActionResult<Object>> GetRatesAndCommentByProductId(int productId, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _rateService.GetRatesAndCommentByProductIdAsync(productId, pageSize, pageIndex);

                return result == null
                    ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                    : Ok(new MyServiceResponse<object>(result));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }



        // PUT: api/RateModelGets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatweRate(int id, RateModelPost model)
        {
            try
            {
                var status = await _rateService.UpdateRateAsync(id, model);


                if (status == MyStatusCode.SUCCESS)
                {
                    var entity = await _rateService.GetRateById(id);

                    return Ok(new MyServiceResponse<RateModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
                }

                return BadRequest(new MyServiceResponse<RateModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500,
                    new MyServiceResponse<RateModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        // POST: api/RateModelGets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RateModelGet>> AddRate(RateModelPost model)
        {
            try
            {
                var status = await _rateService.AddRateAsync(model);
                if (status == MyStatusCode.NOTFOUND_ORDER)
                {
                    return BadRequest(new MyServiceResponse<RateModelGet>(false, MyStatusCode.NOTFOUND_ORDER_RESUL));
                }

                else if (status > 0)
                {


                    return Ok(new MyServiceResponse<RateModelGet>(true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<RateModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<RateModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


            }
        }

        // DELETE: api/RateModelGets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateModelGet(int id)
        {
            try
            {
                await _rateService.DeleteRateAsync(id);
                return Ok(new MyServiceResponse<RateModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            }
            catch
            {
                return StatusCode(500,
                    new MyServiceResponse<RateModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

  
    }
}
