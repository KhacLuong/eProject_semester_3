using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.ViewModels;
using ShradhaBook_API.Services.CategotyService;
using System.Net;
using ShradhaBook_API.Helpers;

namespace ShradhaBook_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        // GET: api/ViewCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModelGet>>> GetAllCategories(string? name, string? code, string? status, int sortBy = 0,int pageIndex =200)
        {
            try
            {
                return Ok(await _categoryService.GetAllCategoryAsync(name, code, status, sortBy,pageIndex));
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ViewCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModelGet>> GetCategory(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }

        }


        //private bool CategoryExists(int id)
        //{
        //   
        //}
    }
}
