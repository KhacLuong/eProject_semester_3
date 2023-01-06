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

    // GET: api/BlogTag
    [HttpGet]
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

    // GET: api/BlogTag/5
    [HttpGet("{id}")]
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