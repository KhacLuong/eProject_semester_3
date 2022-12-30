using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.ProductTagService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTagController : ControllerBase
    {
        private readonly IProductTagService _productTagService;

        public ProductTagController(IProductTagService productTagService)
        {
            _productTagService = productTagService;

        }

        // GET: api/ProductTagModels
        [HttpGet]
        public async Task<ActionResult<Object>> GetAllProductTag(string? prodctName, string? tagName, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _productTagService.GetAllProductTagAsync(prodctName, tagName, pageSize, pageIndex);

                return Ok(new MyServiceResponse<Object>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }


        // GET: api/ProductTagModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTagGet>> GetProductTag(int id)
        {
            try
            {
                var result = await _productTagService.GetProductTagAsync(id);

                return result == null ? NotFound(new MyServiceResponse<ProductTagGet>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<ProductTagGet>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<BlogTagModelGet>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

     

    }
}
