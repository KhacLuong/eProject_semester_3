﻿using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    ///     Get all manufacturers
    /// </summary>
    /// <returns></returns>
    // GET: api/Maufacturer
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    ///     Get manufacturer given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ViewCategories/5
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

    //private bool ViewManufacturerExists(int id)
    //{
    //    return _context.ViewManufacturer.Any(e => e.Id == id);
    //}
}