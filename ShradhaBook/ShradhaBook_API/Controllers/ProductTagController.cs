using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTagController : ControllerBase
{
    private readonly IProductTagService _productTagService;

    public ProductTagController(IProductTagService productTagService)
    {
        _productTagService = productTagService;
    }

    /// <summary>
    ///     Get all product tags
    /// </summary>
    /// <param name="prodctName"></param>
    /// <param name="tagName"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/ProductTagModels
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllProductTag(string? prodctName, string? tagName, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _productTagService.GetAllProductTagAsync(prodctName, tagName, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get product tag given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ProductTagModels/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductTagGet>> GetProductTag(int id)
    {
        try
        {
            var result = await _productTagService.GetProductTagAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<ProductTagGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<ProductTagGet>(result, true, ""));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
}