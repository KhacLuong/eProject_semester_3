using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminProductTagController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductTagService _productTagService;


    public AdminProductTagController(IProductTagService productTagService, IMapper mapper)
    {
        _productTagService = productTagService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get all product tag
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="tagName"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/ProductTagPosts
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllProductTag(string? productName, string? tagName, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _productTagService.GetAllProductTagAsync(productName, tagName, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get a product tag given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ProductTagPosts/5
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
            return StatusCode(500, new MyServiceResponse<ProductTagGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Update product tag
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/ProductTagPosts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductTag(int id, ProductTagPost model)
    {
        try
        {
            var status = await _productTagService.UpdateProductTagAsync(id, model);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<ProductTagGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT +
                    ", There is already a ProductTag of this ProductId and this TagId "));

            if (status == MyStatusCode.NOTFOUND)
                return BadRequest(new MyServiceResponse<ProductTagGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ",  Not found Product or Tag"));

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _productTagService.GetProductTagAsync(id);
                return Ok(new MyServiceResponse<ProductTagGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ProductTagGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<ProductTagGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Create new product tag
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/ProductTagPosts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductTagGet>> AddProductTag(ProductTagPost model)
    {
        try
        {
            var status = await _productTagService.AddProductTagAsync(model);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<ProductTagGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT +
                    ", There is already a ProductTag of this ProductId and this TagId "));
            if (status == MyStatusCode.NOTFOUND)
                return BadRequest(new MyServiceResponse<ProductTagGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ",  Not found Product or Tag"));


            if (status > 0)
            {
                var newEntity = await _productTagService.GetProductTagAsync(status);

                return Ok(new MyServiceResponse<ProductTagGet>(_mapper.Map<ProductTagGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ProductTagGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<ProductTagGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Delete product tag
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/ProductTagPosts/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProductTag(int id)
    {
        try
        {
            await _productTagService.DeleteProductTagAsync(id);
            return Ok(new MyServiceResponse<ProductTagGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<ProductTagGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool ProductTagPostExists(int id)
    //{
    //    return _context.ProductTagPost.Any(e => e.Id == id);
    //}
}