using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    ///     Get all products
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="status"></param>
    /// <param name="categoryName"></param>
    /// <param name="AuthorName"></param>
    /// <param name="manufactuerName"></param>
    /// <param name="lowPrice"></param>
    /// <param name="hightPrice"></param>
    /// <param name="lowQuantity"></param>
    /// <param name="hightQuantity"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/ViewProducts
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    ///     Get product given product id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ViewProducts/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Detail{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    ///     Get products of given category id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("GetProductByCategoryId{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductByCategoryId(int id, int sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetProductByIdCategoryAsync(id, sortBy, pageSize, pageIndex);

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
    ///     Get products of given category slug
    /// </summary>
    /// <param name="slug"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("GetProductByCategorySlug{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductByCategoryId(string slug, int sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetProductBySlugCategoryAsync(slug, sortBy, pageSize, pageIndex);

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
    ///     Get products given author id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("GetProductByAuthorId{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductByAuthorId(int id, int sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetProductByIdAuthorAsync(id, sortBy, pageSize, pageIndex);

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
    ///     Get products given manufacturer id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("GetProductByManufacturer{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductByManufacturer(int id, int sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetProductByIdManufactuerAsync(id, sortBy, pageSize, pageIndex);

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
    ///     Increase product view count
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("IncreaseViewCountProduct{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    ///     Get products in wish list given user id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("GetProductWishListByUserId{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductWishListByUserId(int id, int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetProductWishListByUserIdAsync(id, pageSize, pageIndex);

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
    ///     Get product given slug
    /// </summary>
    /// <param name="slug"></param>
    /// <returns></returns>
    [HttpGet("GetProductBySlug{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetProductBySlug(string slug)
    {
        try
        {
            var result = await _productService.GetProductDetailAsync(slug);

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
    ///     Get product given the lowest price
    /// </summary>
    /// <param name="numberRetrieving"></param>
    /// <returns></returns>
    [HttpGet("GetProductByTheLowestPrice")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductModelGet>>> GetProductByTheLowestPrice(int numberRetrieving)
    {
        try
        {
            var result = await _productService.GetProductByTheLowestPricedAsync(numberRetrieving);

            return result == null
                ? NotFound(new MyServiceResponse<List<ProductModelGet>>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<List<ProductModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<ProductModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get latest product
    /// </summary>
    /// <param name="numberRetrieving"></param>
    /// <returns></returns>
    [HttpGet("GetProductByLatestReleases")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductModelGet>>> GetProductByLatestReleases(int numberRetrieving)
    {
        try
        {
            var result = await _productService.GetProductByLatestReleasesAsync(numberRetrieving);

            return result == null
                ? NotFound(new MyServiceResponse<List<ProductModelGet>>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<List<ProductModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<ProductModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get products given the most view
    /// </summary>
    /// <param name="numberRetrieving"></param>
    /// <returns></returns>
    [HttpGet("GetProductByTheMostView")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductModelGet>>> GetProductByTheMostView(int numberRetrieving)
    {
        try
        {
            var result = await _productService.GetProductByTheMostViewAsync(numberRetrieving);

            return result == null
                ? NotFound(new MyServiceResponse<List<ProductModelGet>>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<List<ProductModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<ProductModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool ViewProductExists(int id)
    //{
    //    return _context.ViewProduct.Any(e => e.Id == id);
    //}
}