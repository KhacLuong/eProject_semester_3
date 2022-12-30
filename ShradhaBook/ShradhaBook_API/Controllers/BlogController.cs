using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.BlogService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IProductService productService, IMapper mapper)
        {
            _blogService = blogService;
            _productService = productService;
            _mapper = mapper;

        }

        // GET: api/BlogModel
        [HttpGet]
        public async Task<ActionResult<object>> GetAllBlog(string? tiltle, string? authorName, string? status, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _blogService.GetAllBlogAsync(tiltle, authorName, status, pageSize = 20, pageIndex = 1);

                return Ok(new MyServiceResponse<object>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<List<object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/BlogModel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModelGet>> GetBlog(int id)
        {
            try
            {
                var result = await _blogService.GetBlogAsync(id);

                return result == null ? NotFound(new MyServiceResponse<BlogModelGet>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<BlogModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
        [HttpGet("Detail{id}")]
        public async Task<ActionResult<object>> GetBlogDetail(int id)
        {
            try
            {
                var result = await _blogService.GetBlogDetailAsync(id);

                return result == null ? NotFound(new MyServiceResponse<object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        [HttpGet("BlogByAuthor{id}")]
        public async Task<ActionResult<object>> GetBlogByAuthorId(int id)
        {
            try
            {
                var result = await _blogService.GetBlogByAuthordIdAsync(id);

                return result == null ? NotFound(new MyServiceResponse<object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        [HttpPost("IncreaseViewCountBlog{id}")]
        public async Task<ActionResult> IncreaseViewCountBlog(int id)
        {
            try
            {
                var result = await _blogService.IncreseCountViewBlogAsync(id);

                return result == true ? Ok(MyStatusCode.SUCCESS_RESULT) : BadRequest(MyStatusCode.FAILURE_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
            }
        }


    }
}
