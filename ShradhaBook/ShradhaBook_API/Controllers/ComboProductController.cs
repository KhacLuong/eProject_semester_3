using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboProductController : ControllerBase
    {
        private readonly IComboProductService _comboProductService;

        public ComboProductController(IComboProductService comboProductService)
        {
            _comboProductService = comboProductService;

        }

        // GET: api/ComboProductModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboProductModel>>> GetAllComboProduct()
        {
            try
            {
                return Ok(await _comboProductService.GetAllComboProductAsync());
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ComboProductModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboProductModel>> GetComboProduct(int id)
        {
            try
            {
                var result = await _comboProductService.GetComboProductAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }


  

        // POST: api/ComboProductModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComboProductModel>> AddComboProduct(ComboProductModel model)
        {
            try
            {
                var status = await _comboProductService.AddComboProductAsync(model);
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
                    return Ok(MyStatusCode.SUCCESS_RESULT);
                }
                return BadRequest(MyStatusCode.FAILURE_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // DELETE: api/ComboProductModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComboProduct(int id)
        {
            try
            {
                await _comboProductService.DeleteComboProductAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }
        //private bool ComboProductModelExists(int id)
        //{
        //    return _context.ComboProductModel.Any(e => e.Id == id);
        //}
    }
}
