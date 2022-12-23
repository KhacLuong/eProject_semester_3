using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.ViewModels;

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
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllCategories(string? name, string? code, int? status = 0, int sortBy = 0)
        {
            try
            {
                return Ok(await _categoryService.GetAllCategoryAsync(name,code,status,sortBy));
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ViewCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
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

        // PUT: api/ViewCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel model)
        {
            try
            {
               
                int status = await _categoryService.UpdateCategoryAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }else if (status == MyStatusCode.FAILURE)
                {
                    return BadRequest(MyStatusCode.FAILURE_RESULT);

                }

                return Ok(MyStatusCode.SUCCESS_RESULT);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }


        }

        // POST: api/ViewCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryModel model)
        {
            try
            {
                var status = await _categoryService.AddCategoryAsync(model);
                if(status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status >0)
                {
                    //var newCategoryId = status;
                    //var category = await _categoryService.GetCategoryAsync(newCategoryId);
                    //return category == null ? NotFound(SUCCESS) : Ok();
                    return Ok(MyStatusCode.SUCCESS_RESULT);
                }
                return BadRequest(MyStatusCode.FAILURE_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }

        }

        // DELETE: api/ViewCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

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
