using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewManufacturersController : ControllerBase
    {
        private readonly DataContext _context;

        public ViewManufacturersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ViewManufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewManufacturer>>> GetViewManufacturer()
        {
            return await _context.ViewManufacturer.ToListAsync();
        }

        // GET: api/ViewManufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewManufacturer>> GetViewManufacturer(int id)
        {
            var viewManufacturer = await _context.ViewManufacturer.FindAsync(id);

            if (viewManufacturer == null)
            {
                return NotFound();
            }

            return viewManufacturer;
        }

        // PUT: api/ViewManufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViewManufacturer(int id, ViewManufacturer viewManufacturer)
        {
            if (id != viewManufacturer.Id)
            {
                return BadRequest();
            }

            _context.Entry(viewManufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewManufacturerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ViewManufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewManufacturer>> PostViewManufacturer(ViewManufacturer viewManufacturer)
        {
            _context.ViewManufacturer.Add(viewManufacturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViewManufacturer", new { id = viewManufacturer.Id }, viewManufacturer);
        }

        // DELETE: api/ViewManufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViewManufacturer(int id)
        {
            var viewManufacturer = await _context.ViewManufacturer.FindAsync(id);
            if (viewManufacturer == null)
            {
                return NotFound();
            }

            _context.ViewManufacturer.Remove(viewManufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViewManufacturerExists(int id)
        {
            return _context.ViewManufacturer.Any(e => e.Id == id);
        }
    }
}
