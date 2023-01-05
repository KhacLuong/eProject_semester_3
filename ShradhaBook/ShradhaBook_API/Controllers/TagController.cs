using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers;

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
    public async Task<ActionResult<object>> GetAllTag(string? name, int sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _tagService.GetAllTagAsync(name, sortBy, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // GET: api/TagModels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TagModelGet>> GetTag(int id)
    {
        try
        {
            var result = await _tagService.GetTagAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<TagModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<TagModelGet>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<TagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

        // GET: api/TagModels/5
        [HttpGet("GetTagsByBlogId{id}")]
        public async Task<ActionResult<Object>> GetTagsByBlogId(int id, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _tagService.GetTagsByBlogId(id, pageSize, pageIndex);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result, true,""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<TagModelGet>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        [HttpGet("Product{id}")]
        public async Task<ActionResult<Object>> GetTagsByProductId(int id, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _tagService.GetTagsByProductId(id,  pageSize,  pageIndex);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result, true, ""));

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