using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminProductController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public AdminProductController(ICategoryService categoryService, IProductService productService, IMapper mapper)
    {
        _categoryService = categoryService;
        _productService = productService;
        _mapper = mapper;
    }

    // GET: api/AdminProduct
    [HttpGet]
    public async Task<ActionResult<object>> GetAllProduc(string? name, string? code, string? status,
        string? categoryName, string? authorName, string? manufactuerName,
        decimal? moreThanPrice, decimal? lessThanPrice, long? moreThanQuantity, long? lessThanQuantity, int? sortBy = 0,
        int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetAllProductAsync(name, code, status, categoryName, authorName,
                manufactuerName,
                moreThanPrice, lessThanPrice, moreThanQuantity, lessThanQuantity, sortBy, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // GET: api/AdminProduct/5
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

    [HttpGet("getDetail{id}")]
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

    // PUT: api/AdminProduct/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductModelPost model)
    {
        try
        {
            var status = await _productService.UpdateProductAsync(id, model);
            if (status == MyStatusCode.DUPLICATE_CODE)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));

            if (status == MyStatusCode.DUPLICATE_NAME)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _productService.GetProductAsync(id);
                return Ok(new MyServiceResponse<ProductModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // POST: api/AdminProduct
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductModelGet>> AddProductModelGet(ProductModelPost model)
    {
        try
        {
            var status = await _productService.AddProductAsync(model);
            if (status == MyStatusCode.DUPLICATE_CODE)
                return BadRequest(new MyServiceResponse<ProductModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));

            if (status == MyStatusCode.DUPLICATE_NAME)
                return BadRequest(new MyServiceResponse<ProductModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));

            if (status > 0)
            {
                var newEntity = await _productService.GetProductAsync(status);

                return Ok(new MyServiceResponse<ProductModelGet>(_mapper.Map<ProductModelGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ProductModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ProductModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // DELETE: api/AdminProduct/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductModelGet(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new MyServiceResponse<ProductModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ProductModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool ProductModelGetExists(int id)
    //{
    //    return _context.ProductModelGet.Any(e => e.Id == id);
    //}
}