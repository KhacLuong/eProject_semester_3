using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.WishListUserService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishListUserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IWishListUserService _wishListUserService;

    public WishListUserController(IWishListUserService wishListUserService, IProductService productService,
        IMapper mapper)
    {
        _wishListUserService = wishListUserService;
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet("GetProductWishListByUserId{id}")]
    public async Task<ActionResult<object>> GetProductWishListByUserId(int id, int pageSize = 20, int pageIndex = 1)
    {
        try
        {
            var result = await _productService.GetProductWishListByUserIdAsync(id, pageSize, pageIndex);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }


    // POST: api/WishListUser
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WishListUserPost>> AddWishListUserG(int userId, int prouctId)
    {
        //try
        {
            var status = await _wishListUserService.AddWishListUserAsync(userId, prouctId);
            if (status == MyStatusCode.DUPLICATE)
            {
                return BadRequest(new MyServiceResponse<WishListUserGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", product already exists in wishlist "));
            }

            if (status > 0)
            {
                var newEntity = await _wishListUserService.GetWishListUserAsync(status);

                return Ok(new MyServiceResponse<WishListUserGet>(_mapper.Map<WishListUserGet>(newEntity), true,
                    MyStatusCode.ADD_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<WishListUserGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        //catch
        //{
        //    return StatusCode(500, new MyServiceResponse<WishListUserGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


        //}
    }

    // DELETE: api/WishListUser/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWishListUserGet(int userId, int prouctId)
    {
        try
        {
            var status = await _wishListUserService.DeleteWishListUserAsync(userId, prouctId);


            if (status == MyStatusCode.SUCCESS)
                return BadRequest(new MyServiceResponse<WishListUserGet>(true, MyStatusCode.DELLETE_FAILURE_RESULT));
            return BadRequest(new MyServiceResponse<WishListUserGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<WishListUserGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
}