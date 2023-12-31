﻿using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    /// <summary>
    ///     Get all authors
    /// </summary>
    /// <returns></returns>
    // GET: api/Authors
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorModelGet>>> GetAllAuthors()
    {
        try
        {
            var result = await _authorService.GetAllAuthorAsync();
            return Ok(new MyServiceResponse<List<AuthorModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<AuthorModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get author given author id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ViewCategories/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorModelGet>> GetCategory(int id)
    {
        try
        {
            var result = await _authorService.GetAuthorAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<AuthorModelGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<AuthorModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
}