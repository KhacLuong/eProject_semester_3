using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService _comboService;

        public ComboController(IComboService comboService)
        {
            _comboService = comboService;

        }


        // GET: api/ComboModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboModel>>> GetAllCombo(string? name, int status = 0, int sortBy = 0)
        {
            try
            {
                return Ok(await _comboService.GetAllComboAsync(name, status, sortBy));
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ComboModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboModel>> GetCombo(int id)
        {
            try
            {
                var result = await _comboService.GetComboAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // PUT: api/ComboModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCombo(int id, ComboModel model)
        {
            try
            {

                int status = await _comboService.UpdateComboAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status == MyStatusCode.FAILURE)
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

        // POST: api/ComboModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddCombo(ComboModel model)
        {
            try
            {
                var status = await _comboService.AddComboAsync(model);
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

        // DELETE: api/ComboModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombo(int id)
        {
            try
            {
                await _comboService.DeleteComboAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }
    }
}
