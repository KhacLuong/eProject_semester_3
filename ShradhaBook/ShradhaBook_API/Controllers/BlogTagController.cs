﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.BlogTagService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTagController : ControllerBase
    {
        private readonly IBlogTagService _blogTagService;

        public BlogTagController(IBlogTagService blogTagService)
        {
            _blogTagService = blogTagService;

        }

        // GET: api/BlogTag
        [HttpGet]
        public async Task<ActionResult<Object>> GetAllBlogTag(string? blogTitle, string? tagName, int pageSize = 20, int pageIndex = 1)
        {
            try
            {
                var result = await _blogTagService.GetAllBlogTagAsync(blogTitle, tagName, pageSize, pageIndex);

                return Ok(new MyServiceResponse<Object>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/BlogTag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogTagModelGet>> GetBlogTag(int id)
        {
            try
            {
                var result = await _blogTagService.GetBlogTagAsync(id);

                return result == null ? NotFound(new MyServiceResponse<BlogTagModelGet>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<BlogTagModelGet>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<BlogTagModelGet>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        
    }
}
