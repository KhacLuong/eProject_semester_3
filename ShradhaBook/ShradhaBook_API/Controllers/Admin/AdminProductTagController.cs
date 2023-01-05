using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;

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

    // GET: api/ProductTagPosts
    [HttpGet]
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

    // GET: api/ProductTagPosts/5
    [HttpGet("{id}")]
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

    // PUT: api/ProductTagPosts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
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

    // POST: api/ProductTagPosts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
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

    // DELETE: api/ProductTagPosts/5
    [HttpDelete("{id}")]
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