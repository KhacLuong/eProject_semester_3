using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.ManufacturerService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;

        }

        // GET: api/ViewManufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerModelGet>>> GetAllManufacturer(string? name, string? code, int page =100)
        {
            try
            {
                return Ok(await _manufacturerService.GetAllManufacturersAsync(name, code,page));
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ViewManufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerModelGet>> GetManufacturer(int id)
        {
            try
            {
                var manufacturer = await _manufacturerService.GetManufacturerAsync(id);

                return manufacturer == null ? NotFound() : Ok(manufacturer);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // PUT: api/ViewManufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManufacturer(int id, ManufacturerModelPost model)
        {
            try
            {

                int status = await _manufacturerService.UpdateManufacturerAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status == MyStatusCode.FAILURE)
                {
                    return BadRequest(MyStatusCode.FAILURE_RESULT);

                }

                return Ok(MyStatusCode.SUCCESS_RESULT);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
            }
        }

        // POST: api/ViewManufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManufacturerModelGet>> AddManufacturer(ManufacturerModelPost model)
        {
            try
            {
                var status = await _manufacturerService.AddManufacturerAsync(model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status > 0)
                {

                    return Ok(MyStatusCode.SUCCESS_RESULT);
                }
                return BadRequest(MyStatusCode.FAILURE_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
            }
        }

        // DELETE: api/ViewManufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViewManufacturer(int id)
        {
            try
            {
                await _manufacturerService.DeleteManufacturerAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        //private bool ViewManufacturerExists(int id)
        //{
        //    return _context.ViewManufacturer.Any(e => e.Id == id);
        //}
    }
}
