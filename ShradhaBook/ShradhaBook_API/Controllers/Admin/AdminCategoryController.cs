using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminCategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public AdminCategoryController(ICategoryService categoryService, IProductService productService, IMapper mapper)
    {
        _categoryService = categoryService;
        _productService = productService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get all category
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="status"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    // GET: api/AdminCategory
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryModelGet>>> GetAllCategories(string? name, string? code,
        string? status = "Active", int sortBy = 0, int pageSize = 20, int page = 1)
    {
        try
        {
            var result = await _categoryService.GetAllCategoryAsync(name, code, status, sortBy, pageSize, page);
            return Ok(new MyServiceResponse<List<CategoryModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<CategoryModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get a given category id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/AdminCategory/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryModelGet>> GetCategory(int id)
    {
        try
        {
            var result = await _categoryService.GetCategoryAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<CategoryModelGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Update given category id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/AdminCategory/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(int id, CategoryModelPost model)
    {
        try
        {
            var status = await _categoryService.UpdateCategoryAsync(id, model);
            if (status == MyStatusCode.DUPLICATE_CODE)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));

            if (status == MyStatusCode.DUPLICATE_NAME)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _categoryService.GetCategoryAsync(id);
                return Ok(new MyServiceResponse<CategoryModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Create new category
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/AdminCategory
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddCategory(CategoryModelPost model)
    {
        //try
        {
            var status = await _categoryService.AddCategoryAsync(model);
            if (status == MyStatusCode.DUPLICATE_CODE)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));

            if (status == MyStatusCode.DUPLICATE_NAME)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));


            if (status > 0)
            {
                //var newCategoryId = status;
                //var category = await _categoryService.GetCategoryAsync(newCategoryId);
                //return category == null ? NotFound(SUCCESS) : Ok();
                var newEntity = await _categoryService.GetCategoryAsync(status);

                return Ok(new MyServiceResponse<CategoryModelGet>(_mapper.Map<CategoryModelGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        //catch
        //{
        //    return StatusCode(500,
        //        new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        //}
    }

    /// <summary>
    ///     Delete given category id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/AdminCategory/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var result = await _productService.CheckExistProductByIdCategoryAsync(id);
            if (result == false)
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok(new MyServiceResponse<CategoryModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                MyStatusCode.DELLETE_FAILURE_RESULT + ", " + "There are already products in this Category"));
        }
        catch
        {
            return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
        }
    }
}