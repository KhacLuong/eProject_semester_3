using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufacturersController : ControllerBase
{
    private readonly IManufacturerService _manufacturerService;

    public ManufacturersController(IManufacturerService manufacturerService)
    {
        _manufacturerService = manufacturerService;
    }

    // GET: api/Maufacturer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManufacturerModelGet>>> GetAllManufacturet()
    {
        try
        {
            var result = await _manufacturerService.GetAllManufacturersAsync();
            return Ok(new MyServiceResponse<List<ManufacturerModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<ManufacturerModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // GET: api/ViewCategories/5
    [HttpGet("{id}")]
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

    //private bool ViewManufacturerExists(int id)
    //{
    //    return _context.ViewManufacturer.Any(e => e.Id == id);
    //}
}