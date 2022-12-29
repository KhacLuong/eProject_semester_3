using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.ProductService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }

        // GET: api/ViewProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetAllProducts(string? name, string? code, string? status, string? categoryName, string? AuthorName, string? manufactuerName,
            decimal? lowPrice, decimal? hightPrice, long? lowQuantity, long? hightQuantity, int? sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _productService.GetAllProductAsync(name, code, status, categoryName, AuthorName, manufactuerName,
           lowPrice, hightPrice, lowQuantity, hightQuantity, sortBy, pageSize, pageIndex);

                return Ok(new MyServiceResponse<Object>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<List<Object>>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/ViewProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModelGet>> GetProduct(int id)
        {
            try
            {
                var result = await _productService.GetProductAsync(id);

                return result == null ? NotFound(new MyServiceResponse<ProductModelGet>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<ProductModelGet>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        [HttpGet("Detail{id}")]

        public async Task<ActionResult<Object>> GetProductDetail(int id)
        {
            try
            {
                var result = await _productService.GetProductDetailAsync(id);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        [HttpGet("Category{id}")]
        public async Task<ActionResult<Object>> GetProductByCategoryId(int id)
        {
            try
            {
                var result = await _productService.GetProductByIdCategoryAsync(id);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        [HttpGet("Author{id}")]
        public async Task<ActionResult<Object>> GetProductByAuthorId(int id)
        {
            try
            {
                var result = await _productService.GetProductByIdAuthorAsync(id);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        [HttpGet("Manufacturer{id}")]
        public async Task<ActionResult<Object>> GetProductByManufacturer(int id)
        {
            try
            {
                var result = await _productService.GetProductByIdManufactuerAsync(id);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        [HttpPost("IncreaseViewCountProduct{id}")]
        public async Task<ActionResult> IncreaseViewCountProduct(int id)
        {
            try
            {
                var result = await _productService.IncreaseViewCountProduct(id);

                return result == true ? Ok(MyStatusCode.SUCCESS_RESULT) : BadRequest(MyStatusCode.FAILURE_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
            }
        }






        //private bool ViewProductExists(int id)
        //{
        //    return _context.ViewProduct.Any(e => e.Id == id);
        //}
    }
}
