using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Services.CommentService;
using ShradhaBook_ClassLibrary.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        // PUT: api/CommentModelPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, CommentModelPost model)
        {
            try
            {
                var status = await _commentService.UpdateCommentAsync(id, model);


                if (status == MyStatusCode.SUCCESS)
                {
                    var entity = await _commentService.GetCommentById(id);

                    return Ok(new MyServiceResponse<CommentModelGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
                }

                return BadRequest(new MyServiceResponse<CommentModelGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500,
                    new MyServiceResponse<CommentModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        // POST: api/CommentModelPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommentModelPost>> AddComment(CommentModelPost model)
        {
            try
            {
                var status = await _commentService.AddCommentAsync(model);
                if (status == MyStatusCode.NOTFOUND_RATE)
                {
                    return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.NOTFOUND_RATE_RESUL));
                }

                else if (status > 0)
                {


                    return Ok(new MyServiceResponse<WishListProductGet>(true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.ADD_FAILURE_RESULT));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListProductGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


            }
        }

        // DELETE: api/CommentModelPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await _commentService.DeleteCommentAsync(id);
                return Ok(new MyServiceResponse<CommentModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            }
            catch
            {
                return StatusCode(500,
                    new MyServiceResponse<CommentModelGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }
    }
}
