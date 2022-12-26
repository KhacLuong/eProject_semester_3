using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.CategotyService;
using ShradhaBook_API.Services.ProductService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public AdminCategoryController(ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;

        }

        // GET: api/AdminCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModelGet>>> GetAllCategories(string? name, string? code, string? status ="Active", int sortBy = 0,int pageSize =20, int page =1)
        {

            try
            {
               var result = await _categoryService.GetAllCategoryAsync(name, code, status, sortBy, pageSize, page);
                return Ok(new MyServiceResponse<List<CategoryModelGet>>(result));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<List<CategoryModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/AdminCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModelGet>> GetCategory(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryAsync(id);

                return result == null ? NotFound(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<CategoryModelGet>(result));
                
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<CategoryModelGet>(false,MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
            

        }

        // PUT: api/AdminCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModelPost model)
        {
            try
            {

                int status = await _categoryService.UpdateCategoryAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT ));
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));
                }
                else if (status == MyStatusCode.SUCCESS)
                {
                    var entity = await _categoryService.GetCategoryAsync(id);
                    return Ok(new MyServiceResponse<CategoryModelGet>(entity,true, MyStatusCode.UPDATE_SUCCESS_RESULT));

                }
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }

        }

        // POST: api/AdminCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryModelPost model)
        {
            try
            {
                var status = await _categoryService.AddCategoryAsync(model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));
                }
                else if (status > 0)
                {
                    //var newCategoryId = status;
                    //var category = await _categoryService.GetCategoryAsync(newCategoryId);
                    //return category == null ? NotFound(SUCCESS) : Ok();
                    var newEntity = await _categoryService.GetCategoryAsync(status);

                    return Ok(new MyServiceResponse<CategoryModelGet>(_mapper.Map<CategoryModelGet>(newEntity),true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


            }

        }
        // DELETE: api/AdminCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _productService.checkExistProductByIdCategoryAsync(id);
                if(result==false)
                {
                    await _categoryService.DeleteCategoryAsync(id);
                    return Ok(new MyServiceResponse<CategoryModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
                }
                else
                {
                    return BadRequest(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT +", "+  "There are already products in this Category"));
                }

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
            }
        }

    }
}
