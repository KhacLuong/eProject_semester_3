using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public BlogController(IBlogService blogService, IProductService productService, IMapper mapper)
    {
        _blogService = blogService;
        _productService = productService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get all blogs
    /// </summary>
    /// <param name="tiltle"></param>
    /// <param name="authorName"></param>
    /// <param name="status"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/BlogModel
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllBlog(string? tiltle, string? authorName, string? status,
        int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _blogService.GetAllBlogAsync(tiltle, authorName, status, pageSize = 20, pageIndex = 1);

            return Ok(new MyServiceResponse<object>(result, true, ""));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get blog given blog id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/BlogModel/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogModelGet>> GetBlog(int id)
    {
        try
        {
            var result = await _blogService.GetBlogAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<BlogModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<BlogModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get blog detail given blog id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Detail{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetBlogDetail(int id)
    {
        try
        {
            var result = await _blogService.GetBlogDetailAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get blog detail given blog slug
    /// </summary>
    /// <param name="slug"></param>
    /// <returns></returns>
    [HttpGet("DetailBySlug{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetBlogDetailBySlug(string slug)
    {
        try
        {
            var result = await _blogService.GetBlogDetailBySlugAsync(slug);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get blog given author id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetBlogByAuthorId{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<object>> GetBlogByAuthorId(int id)
    {
        try
        {
            var result = await _blogService.GetBlogByAuthordIdAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Increase view blog given blog id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("IncreaseViewCountBlog{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> IncreaseViewCountBlog(int id)
    {
        try
        {
            var result = await _blogService.IncreseCountViewBlogAsync(id);

            return result ? Ok(MyStatusCode.SUCCESS_RESULT) : BadRequest(MyStatusCode.FAILURE_RESULT);
        }
        catch
        {
            return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
        }
    }
}