using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Services.CommentService;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    ///     Update comment
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    // PUT: api/CommentModelPosts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    ///     Create new comment
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/CommentModelPosts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommentModelPost>> AddComment(CommentModelPost model)
    {
        try
        {
            var status = await _commentService.AddCommentAsync(model);
            if (status == MyStatusCode.NOTFOUND_RATE)
                return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.NOTFOUND_RATE_RESUL));

            if (status > 0)
                return Ok(new MyServiceResponse<WishListProductGet>(true, MyStatusCode.ADD_SUCCESS_RESULT));
            return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<WishListProductGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Delete comment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/CommentModelPosts/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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