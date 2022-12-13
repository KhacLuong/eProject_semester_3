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
    public class ViewProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ViewProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ViewProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewProduct>>> GetViewProduct()
        {
            return await _context.ViewProduct.ToListAsync();
        }

        // GET: api/ViewProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewProduct>> GetViewProduct(int id)
        {
            var viewProduct = await _context.ViewProduct.FindAsync(id);

            if (viewProduct == null)
            {
                return NotFound();
            }

            return viewProduct;
        }

        // PUT: api/ViewProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViewProduct(int id, ViewProduct viewProduct)
        {
            if (id != viewProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(viewProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewProductExists(id))
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

        // POST: api/ViewProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewProduct>> PostViewProduct(ViewProduct viewProduct)
        {
            _context.ViewProduct.Add(viewProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViewProduct", new { id = viewProduct.Id }, viewProduct);
        }

        // DELETE: api/ViewProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViewProduct(int id)
        {
            var viewProduct = await _context.ViewProduct.FindAsync(id);
            if (viewProduct == null)
            {
                return NotFound();
            }

            _context.ViewProduct.Remove(viewProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViewProductExists(int id)
        {
            return _context.ViewProduct.Any(e => e.Id == id);
        }
    }
}
