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
using ShradhaBook_API.Services.AuthorService;
using ShradhaBook_API.Services.ProductService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public AdminAuthorController(IAuthorService authorService, IProductService productService, IMapper mapper)
        {
            _authorService = authorService;
            _productService = productService;
            _mapper = mapper;

        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<Object>> GetAllAuthor(string? name, string? phone, int? sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {
            //try
            {
                var result = await _authorService.GetAllAuthorAsync(name, phone, sortBy, pageSize, pageIndex);

                return Ok(new MyServiceResponse<Object>(result));

            }
            //catch
            //{
            //    return StatusCode(500, new MyServiceResponse<List<Object>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            //}
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorModelGet>> GetAuthor(int id)
        {
            try
            {
                var result = await _authorService.GetAuthorAsync(id);

                return result == null ? NotFound(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<AuthorModelGet>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        // PUT: api/Author/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorModelPost model)
        {
            try
            {
                int status = await _authorService.UpdateAuthorAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_EMAIL)
                {
                    return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_EMAIL_RESULT));
                }
                else if (status == MyStatusCode.DUPLICATE_PHONE)
                {
                    return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_PHONE_RESULT));
                }
                else if (status == MyStatusCode.SUCCESS)
                {
                    var entity = await _authorService.GetAuthorAsync(id);
                    return Ok(new MyServiceResponse<AuthorModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));

                }
                return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }



        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorModelGet>> AddAuthor(AuthorModelPost model)
        {
            try
            {
                var status = await _authorService.AddAuthorAsync(model);
                if (status == MyStatusCode.DUPLICATE_EMAIL)
                {
                    return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_EMAIL_RESULT));
                }
                else if (status == MyStatusCode.DUPLICATE_PHONE)
                {
                    return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", " + MyStatusCode.DUPLICATE_PHONE_RESULT));

                }
                else if (status > 0)
                {

                    var newEntity = await _authorService.GetAuthorAsync(status);

                    return Ok(new MyServiceResponse<AuthorModelGet>(_mapper.Map<AuthorModelGet>(newEntity), true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.ADD_FAILURE_RESULT));


            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }
        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id, int pageSize = 20, int pageIndex = 1)
        {

            try
            {
                var result = await _productService.checkExistProductByIdAuthorAsync(id);
                if (result == false)
                {
                    await _authorService.DeleteAuthorAsync(id);
                    return Ok(new MyServiceResponse<AuthorModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
                }
                else
                {
                    return BadRequest(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT + ", " + "There are already products in this Author"));
                }

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
    }
}
