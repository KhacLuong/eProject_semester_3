using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Services.TagService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;

        }

        // GET: api/TagModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagModel>>> GetAllTag(string? name, int sortBy = 0)
        {
            try
            {
                return Ok(await _tagService.GetAllTagAsync(name, sortBy));
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/TagModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagModel>> GetTag(int id)
        {
            try
            {
                var result = await _tagService.GetTagAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // PUT: api/TagModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, TagModel model)
        {
            try
            {

                int status = await _tagService.UpdateTagAsync(id, model);
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

        // POST: api/TagModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagModel>> AddTag(TagModel model)
        {
            try
            {
                var status = await _tagService.AddTagAsync(model);
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
        // DELETE: api/TagModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRag(int id)
        {
            try
            {
                await _tagService.DeleteTagAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        //private bool TagModelExists(int id)
        //{
        //    return _context.TagModel.Any(e => e.Id == id);
        //}
    }
}
