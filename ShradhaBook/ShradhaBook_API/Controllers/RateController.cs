//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ShradhaBook_API.Data;
//using ShradhaBook_API.ViewModels;

//namespace ShradhaBook_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RateController : ControllerBase
//    {
//        private readonly ICategoryService _categoryService;

//        public CategoriesController(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }

//        // GET: api/RateModelGets
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<RateModelGet>>> GetRateModelGet()
//        {
//            return await _context.RateModelGet.ToListAsync();
//        }

//        // GET: api/RateModelGets/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<RateModelGet>> GetRateModelGet(int id)
//        {
//            var rateModelGet = await _context.RateModelGet.FindAsync(id);

//            if (rateModelGet == null)
//            {
//                return NotFound();
//            }

//            return rateModelGet;
//        }

//        // PUT: api/RateModelGets/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRateModelGet(int id, RateModelGet rateModelGet)
//        {
//            if (id != rateModelGet.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(rateModelGet).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!RateModelGetExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/RateModelGets
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<RateModelGet>> PostRateModelGet(RateModelGet rateModelGet)
//        {
//            _context.RateModelGet.Add(rateModelGet);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetRateModelGet", new { id = rateModelGet.Id }, rateModelGet);
//        }

//        // DELETE: api/RateModelGets/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRateModelGet(int id)
//        {
//            var rateModelGet = await _context.RateModelGet.FindAsync(id);
//            if (rateModelGet == null)
//            {
//                return NotFound();
//            }

//            _context.RateModelGet.Remove(rateModelGet);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool RateModelGetExists(int id)
//        {
//            return _context.RateModelGet.Any(e => e.Id == id);
//        }
//    }
//}
