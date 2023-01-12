using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Services.WishListProductService;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishListProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IWishListProductService _wishListProductService;

    public WishListProductController(IWishListProductService wishListProductService, IProductService productService,
        IMapper mapper)
    {
        _wishListProductService = wishListProductService;
        _productService = productService;
        _mapper = mapper;
    }

    //[HttpGet("GetProductWishListByUserId{id}")]
    //public async Task<ActionResult<Object>> GetProductWishListByUserId(int userId, int pageSize = 20, int pageIndex = 1)
    //{
    //    try
    //    {
    //        var result = await _productService.GetProductWishListByUserIdAsync(userId, pageSize, pageIndex);

    //        return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

    //    }
    //    catch
    //    {
    //        return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
    //    }
    //}

    /// <summary>
    ///     Create new wish list given user id and product id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="prouctId"></param>
    /// <returns></returns>
    // POST: api/WishListProduct
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WishListProductPost>> AddWishListProductAsync(int userId, int prouctId)
    {
        //try
        {
            var status = await _wishListProductService.AddWishListProductAsync(userId, prouctId);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<WishListProductGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", product already exists in wishlist "));

            if (status > 0)
                return Ok(new MyServiceResponse<WishListProductGet>(true, MyStatusCode.ADD_SUCCESS_RESULT));
            return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.ADD_FAILURE_RESULT));
        }
        //catch
        //{
        //    return StatusCode(500, new MyServiceResponse<WishListProductGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


        //}
    }

    /// <summary>
    ///     Delete product in user wish list given user id and product id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="prouctId"></param>
    /// <returns></returns>
    // DELETE: api/WishListUser/5
    [HttpDelete]
    public async Task<IActionResult> DeleteWishListUserGet(int userId, int prouctId)
    {
        try
        {
            var status = await _wishListProductService.DeleteWishListProductAsync(userId, prouctId);


            if (status == MyStatusCode.SUCCESS)
                return Ok(new MyServiceResponse<WishListProductGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<WishListProductGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    /// <summary>
    ///     Get wish list and cart items given user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("GetTotalWishListAndCart{userId}")]
    public async Task<ActionResult<object>> GetTotalWishListAndCart(int userId)
    {
        try
        {
            var result = await _wishListProductService.GetCountWishListAndCart(userId);

            return result == null
                ? NotFound(new MyServiceResponse<object>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<object>(result));
        }
        catch
        {
            return StatusCode(500, new MyServiceResponse<object>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }
}