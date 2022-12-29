using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/ViewProducts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAllProducts(string? name, string? code, string? status,
        string? categoryName, string? AuthorName, string? manufactuerName,
        decimal? lowPrice, decimal? hightPrice, long? lowQuantity, long? hightQuantity, int? sortBy = 0,
        int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetAllProductAsync(name, code, status, categoryName, AuthorName,
                manufactuerName,
                lowPrice, hightPrice, lowQuantity, hightQuantity, sortBy, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // GET: api/ViewProducts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductModelGet>> GetProduct(int id)
    {
        try
        {
            var result = await _productService.GetProductAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<ProductModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ProductModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    [HttpGet("Detail{id}")]
    public async Task<ActionResult<object>> GetProductDetail(int id)
    {
        try
        {
            var result = await _productService.GetProductDetailAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    [HttpGet("Category{id}")]
    public async Task<ActionResult<object>> GetProductByCategoryId(int id)
    {
        try
        {
            var result = await _productService.GetProductByIdCategoryAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    [HttpGet("Author{id}")]
    public async Task<ActionResult<object>> GetProductByAuthorId(int id)
    {
        try
        {
            var result = await _productService.GetProductByIdAuthorAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    [HttpGet("Manufacturer{id}")]
    public async Task<ActionResult<object>> GetProductByManufacturer(int id)
    {
        try
        {
            var result = await _productService.GetProductByIdManufactuerAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    [HttpPost("IncreaseViewCountProduct{id}")]
    public async Task<ActionResult> IncreaseViewCountProduct(int id)
    {
        try
        {
            var result = await _productService.IncreaseViewCountProduct(id);

            return result ? Ok(MyStatusCode.SUCCESS_RESULT) : BadRequest(MyStatusCode.FAILURE_RESULT);
        }
        catch
        {
            return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
        }
    }


    //private bool ViewProductExists(int id)
    //{
    //    return _context.ViewProduct.Any(e => e.Id == id);
    //}
}