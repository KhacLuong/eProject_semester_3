using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
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
        public async Task<ActionResult<Object>> GetAllTag(string? name, int sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _tagService.GetAllTagAsync( name, sortBy, pageSize, pageIndex);

                return Ok(new MyServiceResponse<Object>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/TagModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagModelGet>> GetTag(int id)
        {
            try
            {
                var result = await _tagService.GetTagAsync(id);

                return result == null ? NotFound(new MyServiceResponse<TagModelGet>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<TagModelGet>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<TagModelGet>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }


        //private bool TagModelExists(int id)
        //{
        //    return _context.TagModel.Any(e => e.Id == id);
        //}
    }
}
