using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminBlogTagController : ControllerBase
{
    private readonly IBlogTagService _blogTagService;
    private readonly IMapper _mapper;


    public AdminBlogTagController(IBlogTagService blogTagService, IMapper mapper)
    {
        _blogTagService = blogTagService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get all blog tag
    /// </summary>
    /// <param name="blogTitle"></param>
    /// <param name="tagName"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/BlogTagModelGets
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
    ///     Get blog tag given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/BlogTagModelGets/5
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

    /// <summary>
    ///     Update blog tag given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/BlogTagModelGets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBlogTag(int id, BlogTagModelPost model)
    {
        //try
        {
            var status = await _blogTagService.UpdateBlogTagAsync(id, model);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<BlogTagModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT +
                    ", There is already a BlogTag of this BlogId and this TagId "));

            if (status == MyStatusCode.NOTFOUND)
                return BadRequest(new MyServiceResponse<BlogTagModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ",  Not found Blog or Tag"));

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _blogTagService.GetBlogTagAsync(id);
                return Ok(new MyServiceResponse<BlogTagModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        //catch
        //{
        //    return StatusCode(500, new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

        //}
    }

    /// <summary>
    ///     Create new blog tag
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/BlogTagModelGets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlogTagModelGet>> AddBlogTag(BlogTagModelPost model)
    {
        try
        {
            var status = await _blogTagService.AddBlogTagAsync(model);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<BlogTagModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT +
                    ", There is already a BlogTag of this BlogId and this TagId "));

            if (status == MyStatusCode.NOTFOUND)
                return BadRequest(new MyServiceResponse<BlogTagModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ",  Not found Blog or Tag"));


            if (status > 0)
            {
                var newEntity = await _blogTagService.GetBlogTagAsync(status);

                return Ok(new MyServiceResponse<BlogTagModelGet>(_mapper.Map<BlogTagModelGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Delete blog tag given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/BlogTag/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBlogTag(int id)
    {
        try
        {
            await _blogTagService.DeleteBlogTagAsync(id);
            return Ok(new MyServiceResponse<BlogTagModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<BlogTagModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool BlogTagModelGetExists(int id)
    //{
    //    return _context.BlogTagModelGet.Any(e => e.Id == id);
    //}
}