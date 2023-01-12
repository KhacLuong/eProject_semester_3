using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminManufacturerController : ControllerBase
{
    private readonly IManufacturerService _manufacturerService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;


    public AdminManufacturerController(IManufacturerService manufacturerService, IProductService productService,
        IMapper mapper)
    {
        _manufacturerService = manufacturerService;
        _productService = productService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get all manufacturer
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageindex"></param>
    /// <returns></returns>
    // GET: api/ManufacturerModels
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetAllManufacturer(string? name, string? code, int pageSize = 20,
        int pageindex = 1)
    {
        try
        {
            var result = await _manufacturerService.GetAllManufacturersAsync(name, code, pageSize, pageindex);

            return Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get a given manufacturer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ManufacturerModels/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ManufacturerModelGet>> GetManufacturer(int id)
    {
        try
        {
            var result = await _manufacturerService.GetManufacturerAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<ManufacturerModelGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Update a given manufacturer id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/ManufacturerModels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateManufacturer(int id, ManufacturerModelPost model)
    {
        try
        {
            var status = await _manufacturerService.UpdateManufacturerAsync(id, model);
            if (status == MyStatusCode.EMAIL_INVALID)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.EMAIL_INVALID_RESULT));
            if (status == MyStatusCode.PHONE_INVALID)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.PHONE_INVALID_RESULT));
            if (status == MyStatusCode.DUPLICATE_CODE)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));

            if (status == MyStatusCode.DUPLICATE_NAME)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));

            if (status == MyStatusCode.DUPLICATE_EMAIL)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_EMAIL_RESULT));

            if (status == MyStatusCode.DUPLICATE_PHONE)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_PHONE_RESULT));

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _manufacturerService.GetManufacturerAsync(id);
                return Ok(new MyServiceResponse<ManufacturerModelGet>(entity, true,
                    MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Create new manufacturer
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/ManufacturerModels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ManufacturerModelPost>> AddManufacturer(ManufacturerModelPost model)
    {
        try
        {
            var status = await _manufacturerService.AddManufacturerAsync(model);
            if (status == MyStatusCode.EMAIL_INVALID)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.EMAIL_INVALID_RESULT));
            if (status == MyStatusCode.PHONE_INVALID)
                return BadRequest(new MyServiceResponse<CategoryModelGet>(false,
                    MyStatusCode.PHONE_INVALID_RESULT));
            if (status == MyStatusCode.DUPLICATE_CODE)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_CODE_RESULT));

            if (status == MyStatusCode.DUPLICATE_NAME)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_NAME_RESULT));

            if (status == MyStatusCode.DUPLICATE_EMAIL)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_EMAIL_RESULT));

            if (status == MyStatusCode.DUPLICATE_PHONE)
                return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_PHONE_RESULT));

            if (status > 0)
            {
                var newEntity = await _manufacturerService.GetManufacturerAsync(status);

                return Ok(new MyServiceResponse<ManufacturerModelGet>(_mapper.Map<ManufacturerModelGet>(newEntity),
                    true, MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Delete a given manufacturer id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/ManufacturerModels/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteManufacturer(int id)
    {
        try
        {
            var result = await _productService.CheckExistProductByIdManufactuerAsync(id);
            if (result == false)
            {
                await _manufacturerService.DeleteManufacturerAsync(id);
                return Ok(new MyServiceResponse<ManufacturerModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<ManufacturerModelGet>(false,
                MyStatusCode.DELLETE_FAILURE_RESULT + ", " + "There are already products in this Manufacturer"));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<ManufacturerModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    //private bool ManufacturerModelExists(int id)
    //{
    //    return _context.ManufacturerModel.Any(e => e.Id == id);
    //}
}