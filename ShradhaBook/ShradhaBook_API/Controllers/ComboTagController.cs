using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.ComboTagService;
﻿using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboTagController : ControllerBase
    {
        private readonly IComboTagService _comboTagService;

        public ComboTagController(IComboTagService comboTagService)
        {
            _comboTagService = comboTagService;

        }
        // GET: api/ComboTagModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboTagModel>>> GetAllComboTag()
        {
            try
            {
                return Ok(await _comboTagService.GetAllComboTagAsync());
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // GET: api/ComboTagModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboTagModel>> GetComboTag(int id)
        {
            try
            {
                var result = await _comboTagService.GetComboTagAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // PUT: api/ComboTagModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComboTag(int id, ComboTagModel model)
        {
            try
            {

                int status = await _comboTagService.UpdateComboTagAsync(id, model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status == MyStatusCode.FAILURE)
                {
                    return BadRequest(MyStatusCode.FAILURE_RESULT);

                }

                return Ok(MyStatusCode.SUCCESS_RESULT);
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // POST: api/ComboTagModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComboTagModel>> AddComboTag(ComboTagModel model)
        {
            try
            {
                var status = await _comboTagService.AddComboTagAsync(model);
                if (status == MyStatusCode.DUPLICATE_CODE)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_CODE_RESULT);
                }
                else if (status == MyStatusCode.DUPLICATE_NAME)
                {
                    return BadRequest(MyStatusCode.DUPLICATE_NAME_RESULT);
                }
                else if (status > 0)
                {
                    //var newCategoryId = status;
                    //var category = await _categoryService.GetCategoryAsync(newCategoryId);
                    //return category == null ? NotFound(SUCCESS) : Ok();
                    return Ok(MyStatusCode.SUCCESS_RESULT);
                }
                return BadRequest(MyStatusCode.FAILURE_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }

        // DELETE: api/ComboTagModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComboTag(int id)
        {
            try
            {
                await _comboTagService.DeleteComboTagAsync(id);
                return Ok(MyStatusCode.SUCCESS_RESULT);

            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);

            }
        }
    }
}
