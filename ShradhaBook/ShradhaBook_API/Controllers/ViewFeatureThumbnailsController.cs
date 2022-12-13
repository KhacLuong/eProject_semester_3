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
    public class ViewFeatureThumbnailsController : ControllerBase
    {
        private readonly DataContext _context;

        public ViewFeatureThumbnailsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ViewFeatureThumbnails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewFeatureThumbnail>>> GetViewFeatureThumbnail()
        {
            return await _context.ViewFeatureThumbnail.ToListAsync();
        }

        // GET: api/ViewFeatureThumbnails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewFeatureThumbnail>> GetViewFeatureThumbnail(int id)
        {
            var viewFeatureThumbnail = await _context.ViewFeatureThumbnail.FindAsync(id);

            if (viewFeatureThumbnail == null)
            {
                return NotFound();
            }

            return viewFeatureThumbnail;
        }

        // PUT: api/ViewFeatureThumbnails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViewFeatureThumbnail(int id, ViewFeatureThumbnail viewFeatureThumbnail)
        {
            if (id != viewFeatureThumbnail.Id)
            {
                return BadRequest();
            }

            _context.Entry(viewFeatureThumbnail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewFeatureThumbnailExists(id))
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

        // POST: api/ViewFeatureThumbnails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewFeatureThumbnail>> PostViewFeatureThumbnail(ViewFeatureThumbnail viewFeatureThumbnail)
        {
            _context.ViewFeatureThumbnail.Add(viewFeatureThumbnail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViewFeatureThumbnail", new { id = viewFeatureThumbnail.Id }, viewFeatureThumbnail);
        }

        // DELETE: api/ViewFeatureThumbnails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViewFeatureThumbnail(int id)
        {
            var viewFeatureThumbnail = await _context.ViewFeatureThumbnail.FindAsync(id);
            if (viewFeatureThumbnail == null)
            {
                return NotFound();
            }

            _context.ViewFeatureThumbnail.Remove(viewFeatureThumbnail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViewFeatureThumbnailExists(int id)
        {
            return _context.ViewFeatureThumbnail.Any(e => e.Id == id);
        }
    }
}
