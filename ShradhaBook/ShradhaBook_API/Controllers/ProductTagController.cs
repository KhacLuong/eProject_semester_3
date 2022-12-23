using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTagController : ControllerBase
    {
        private readonly IProductTagService _productTagService;

        public ProductTagController(IProductTagService productTagService)
        {
            _productTagService = productTagService;

        }

        // GET: api/ProductTagModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTagModel>>> GetAllProductTag()
        {
            try
            {
                return Ok(await _productTagService.GetAllProductTagAsync());
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ProductTagModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTagModel>> GetProductTag(int id)
        {
            try
            {
                var result = await _productTagService.GetProductTagAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // PUT: api/ProductTagModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // POST: api/ProductTagModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductTagModel>> AddProductTag(ProductTagModel model)
        {
            try
            {
                var status = await _productTagService.AddProductTagAsync(model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status > 0)
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

        // DELETE: api/ProductTagModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTag(int id)
        {
            try
            {
                await _productTagService.DeleteProductTagAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }


    }
}
