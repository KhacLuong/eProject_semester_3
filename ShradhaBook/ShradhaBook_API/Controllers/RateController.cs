using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RateController : ControllerBase
{
    private readonly IRateService _rateService;

    public RateController(IRateService rateService)
    {
        _rateService = rateService;
    }

    /// <summary>
    ///     Get rating of given product id
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/RateModelGets
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetRatesByProductId(int productId, int pageSize = 20, int pageIndex = 1)
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

    /// <summary>
    ///     Get rating and comment of given product id
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/RateModelGets
    [HttpGet("GetRateAndComment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetRatesAndCommentByProductId(int productId, int pageSize = 20,
        int pageIndex = 1)
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

    /// <summary>
    ///     Update rating
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/RateModelGets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    ///     Create new rating
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/RateModelGets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RateModelGet>> AddRate(RateModelPost model)
    {
        try
        {
            var status = await _rateService.AddRateAsync(model);
            if (status == MyStatusCode.NOTFOUND_ORDER)
                return BadRequest(new MyServiceResponse<RateModelGet>(false, MyStatusCode.NOTFOUND_ORDER_RESUL));

            if (status > 0) return Ok(new MyServiceResponse<RateModelGet>(true, MyStatusCode.ADD_SUCCESS_RESULT));
            return BadRequest(new MyServiceResponse<RateModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<RateModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Delete rating given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/RateModelGets/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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