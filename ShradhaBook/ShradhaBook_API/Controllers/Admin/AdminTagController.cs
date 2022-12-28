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
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminTagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public AdminTagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;

        }
        // GET: api/TagModelGets
        [HttpGet]
        public async Task<ActionResult<Object>> GetTag(string? name, int sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {

            try
            {
                var result = await _tagService.GetAllTagAsync(name,sortBy,pageSize,pageIndex);
                return Ok(new MyServiceResponse<Object>(result));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/TagModelGets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagModelGet>> GetTag(int id)
        {
            try
            {
                var result = await _tagService.GetTagAsync(id);

                return result == null ? NotFound(new MyServiceResponse<TagModelGet>(false, MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<TagModelGet>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<TagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        // PUT: api/TagModelGets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, TagModelPost model)
        {
            try
            {
                int status = await _tagService.UpdateTagAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(new MyServiceResponse<TagModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));
                }
                else if (status == MyStatusCode.SUCCESS)
                {
                    var entity = await _tagService.GetTagAsync(id);
                    return Ok(new MyServiceResponse<TagModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));

                }
                return BadRequest(new MyServiceResponse<TagModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<TagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
            return NoContent();
        }

        // POST: api/TagModelGets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagModelGet>> AddTag(TagModelPost model)
        {
            try
            {
                var status = await _tagService.AddTagAsync(model);
                if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(new MyServiceResponse<TagModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));
                }
                else if (status > 0)
                {

                    var newEntity = await _tagService.GetTagAsync(status);

                    return Ok(new MyServiceResponse<TagModelGet>(_mapper.Map<TagModelGet>(newEntity), true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<TagModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));


            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<TagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // DELETE: api/TagModelGets/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTagModelGet(int id)
        //{
        //    try
        //    {
        //        var result = await _productService.checkExistProductByIdAuthorAsync(id);
        //        if (result == false)
        //        {
        //            await _authorService.DeleteAuthorAsync(id);
        //            return Ok(new MyServiceResponse<AuthorModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
        //        }
        //        else
        //        {
        //            return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT + ", " + "There are already products in this Author"));
        //        }

        //    }
        //    catch
        //    {
        //        return StatusCode(500, new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        //    }
        //}

        //private bool TagModelGetExists(int id)
        //{
        //    return _context.TagModelGet.Any(e => e.Id == id);
        //}
    }
}
