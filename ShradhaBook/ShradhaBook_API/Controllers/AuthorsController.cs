using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;

        }
        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorModelGet>>> GetAllAuthors()
        {

            try
            {
                var result = await _authorService.GetAllAuthorAsync();
                return Ok(new MyServiceResponse<List<AuthorModelGet>>(result));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<List<AuthorModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }

        }

        // GET: api/ViewCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorModelGet>> GetCategory(int id)
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


    }
}
