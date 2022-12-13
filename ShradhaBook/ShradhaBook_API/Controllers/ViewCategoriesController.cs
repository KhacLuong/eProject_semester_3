using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Services;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewCategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly CategoryService _categoryService;

        //public ViewCategoriesController(DataContext context, CategoryService categoryService)
        //{
        //    _context = context;
        //    _categoryService = categoryService;

        //}

        public ViewCategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
  
        // GET: api/ViewCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewCategory>>> GetViewCategory(string? name, string? code, int? status = 0, int sortBy =0)
        {

            var _allCategories = _categoryService.getViewCategories(name, code, status,sortBy);
            IEnumerable<ViewCategory> allCategories = (IEnumerable<ViewCategory>)_allCategories;
            //    .Where(m => m.Name.ToLower().Contains(String.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
            //    && m.Code.ToLower().Contains(String.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
            //    .Where(m => m.Status == status).ToListAsync();

            //switch (sortBy)
            //{
            //    case 0:
            //        allCategories = (List<Category>)allCategories.OrderBy(m => m.Name);
            //        break;
            //    case 1:
            //        allCategories = (List<Category>)allCategories.OrderBy(m => m.Code);//order by year asc by default
            //        break;

            //    default:
            //        allCategories = (List<Category>)allCategories.OrderBy(m => m.Name);
            //        break;
            //}
            //    var pageEmployee = new Page<Employee>(allEmployees, page, pageSize);
            //    return new RequestResponse<Employee>(HttpStatusCode.OK, "OK", new Dictionary<string, object>()
            //{
            //    {"data",Page<Employee>.Create(allEmployees,pageEmployee)},
            //    {"page", pageEmployee},
            //});

            return Ok(allCategories); 
        }

        // GET: api/ViewCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewCategory>> GetViewCategory(int id)
        {
            var viewCategory = await _context.ViewCategory.FindAsync(id);

            if (viewCategory == null)
            {
                return NotFound();
            }

            return viewCategory;
        }

        // PUT: api/ViewCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViewCategory(int id, ViewCategory viewCategory)
        {
            if (id != viewCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(viewCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewCategoryExists(id))
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

        // POST: api/ViewCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewCategory>> PostViewCategory(ViewCategory viewCategory)
        {
            _context.ViewCategory.Add(viewCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViewCategory", new { id = viewCategory.Id }, viewCategory);
        }

        // DELETE: api/ViewCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViewCategory(int id)
        {
            var viewCategory = await _context.ViewCategory.FindAsync(id);
            if (viewCategory == null)
            {
                return NotFound();
            }

            _context.ViewCategory.Remove(viewCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViewCategoryExists(int id)
        {
            return _context.ViewCategory.Any(e => e.Id == id);
        }
    }
}
