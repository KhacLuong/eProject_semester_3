using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryModelGet>>> GetAllCategories()
    {
        try
        {
            var result = await _categoryService.GetAllCategoryAsync();
            return Ok(new MyServiceResponse<List<CategoryModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<CategoryModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // GET: api/ViewCategories/5
    [HttpGet("{id}")]
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


    //private bool CategoryExists(int id)
    //{
    //   
    //}
}