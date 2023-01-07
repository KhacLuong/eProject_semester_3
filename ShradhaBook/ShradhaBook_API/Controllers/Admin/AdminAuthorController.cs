using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class AdminAuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;


    public AdminAuthorController(IAuthorService authorService, IProductService productService, IMapper mapper)
    {
        _authorService = authorService;
        _productService = productService;
        _mapper = mapper;
    }
    /// <summary>
    /// Return all author 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="phone"></param>
    /// <param name="sortBy"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    // GET: api/Author
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllAuthor(string? name, string? phone, int? sortBy = 0,
        int pageSize = 20, int pageIndex = 1)
    {
        //try
        {
            var result = await _authorService.GetAllAuthorAsync(name, phone, sortBy, pageSize, pageIndex);

            return Ok(new MyServiceResponse<object>(result));
        }
        //catch
        //{
        //    return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        //}
    }
    /// <summary>
    /// Return author given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/Author/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorModelGet>> GetAuthor(int id)
    {
        try
        {
            var result = await _authorService.GetAuthorAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<AuthorModelGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
    /// <summary>
    /// Update author given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/Author/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorModelPost model)
    {
        try
        {
            var status = await _authorService.UpdateAuthorAsync(id, model);
            if (status == MyStatusCode.DUPLICATE_EMAIL)
                return BadRequest(new MyServiceResponse<AuthorModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_EMAIL_RESULT));

            if (status == MyStatusCode.DUPLICATE_PHONE)
                return BadRequest(new MyServiceResponse<AuthorModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_PHONE_RESULT));

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _authorService.GetAuthorAsync(id);
                return Ok(new MyServiceResponse<AuthorModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }


    // POST: api/Author
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorModelGet>> AddAuthor(AuthorModelPost model)
    {
        try
        {
            var status = await _authorService.AddAuthorAsync(model);
            if (status == MyStatusCode.DUPLICATE_EMAIL)
                return BadRequest(new MyServiceResponse<AuthorModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_EMAIL_RESULT));

            if (status == MyStatusCode.DUPLICATE_PHONE)
                return BadRequest(new MyServiceResponse<AuthorModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_PHONE_RESULT));

            if (status > 0)
            {
                var newEntity = await _authorService.GetAuthorAsync(status);

                return Ok(new MyServiceResponse<AuthorModelGet>(_mapper.Map<AuthorModelGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id, int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _productService.CheckExistProductByIdAuthorAsync(id);
            if (result == false)
            {
                await _authorService.DeleteAuthorAsync(id);
                return Ok(new MyServiceResponse<AuthorModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<AuthorModelGet>(false,
                MyStatusCode.DELLETE_FAILURE_RESULT + ", " + "There are already products in this Author"));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
}