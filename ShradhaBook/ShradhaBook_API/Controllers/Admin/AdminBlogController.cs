using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminBlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public AdminBlogController(IBlogService blogService, IProductService productService, IMapper mapper)
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
    // GET: api/AdminBlog
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllBlogs(string? tiltle, string? authorName, string? status,
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
    ///     Get a blog given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/AdminBlog/5
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
    ///     Get blog details
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
    ///     Update blog given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/AdminBlog/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBlog(int id, BlogModelPost model)
    {
        try
        {
            var status = await _blogService.UpdateBlogAsync(id, model);


            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _blogService.GetBlogAsync(id);
                return Ok(new MyServiceResponse<BlogModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<BlogModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<BlogModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Create new blog
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/AdminBlog
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlogModelGet>> AddBlogModelPost(BlogModelPost model)
    {
        try
        {
            var status = await _blogService.AddBlogAsync(model);


            if (status > 0)
            {
                var newEntity = await _blogService.GetBlogAsync(status);

                return Ok(new MyServiceResponse<BlogModelGet>(_mapper.Map<BlogModelGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<BlogModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<BlogModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Delete blog given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/AdminBlog/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        try
        {
            await _blogService.DeleteBlogAsync(id);
            return Ok(new MyServiceResponse<BlogModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<BlogModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool BlogModelPostExists(int id)
    //{
    //    return _context.BlogModelPost.Any(e => e.Id == id);
    //}
}