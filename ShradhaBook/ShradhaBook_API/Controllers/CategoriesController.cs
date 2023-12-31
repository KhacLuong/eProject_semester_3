﻿using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    ///     Get all categories
    /// </summary>
    /// <returns></returns>
    // GET: api/Categories
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryModelGet>>> GetAllCategories()
    {
        try
        {
            var result = await _categoryService.GetAllCategoryAsync();
            return Ok(new MyServiceResponse<List<CategoryModelGet>>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<List<CategoryModelGet>>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get category given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/ViewCategories/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryModelGet>> GetCategory(int id)
    {
        try
        {
            var result = await _categoryService.GetCategoryAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<CategoryModelGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<CategoryModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }


    //private bool CategoryExists(int id)
    //{
    //   
    //}
}