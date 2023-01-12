using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    ///     Get all tags
    /// </summary>
    /// <param name="name"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/TagModels
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    ///     Get tag given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/TagModels/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    ///     Get tag given blog id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/TagModels/5
    [HttpGet("GetTagsByBlogId{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetTagsByBlogId(int id, int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _tagService.GetTagsByBlogId(id, pageSize, pageIndex);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<TagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get tag given product id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("Product{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetTagsByProductId(int id, int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _tagService.GetTagsByProductId(id, pageSize, pageIndex);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<TagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool TagModelExists(int id)
    //{
    //    return _context.TagModel.Any(e => e.Id == id);
    //}
}