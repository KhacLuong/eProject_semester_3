using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogTagController : ControllerBase
{
    private readonly IBlogTagService _blogTagService;

    public BlogTagController(IBlogTagService blogTagService)
    {
        _blogTagService = blogTagService;
    }

    /// <summary>
    ///     Get all blog tags
    /// </summary>
    /// <param name="blogTitle"></param>
    /// <param name="tagName"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/BlogTag
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllBlogTag(string? blogTitle, string? tagName, int pageSize = 20,
        int pageIndex = 1)
    {
        try
        {
            var result = await _blogTagService.GetAllBlogTagAsync(blogTitle, tagName, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get blog tag given blog id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/BlogTag/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogTagModelGet>> GetBlogTag(int id)
    {
        try
        {
            var result = await _blogTagService.GetBlogTagAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<BlogTagModelGet>(result, true, ""));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
}