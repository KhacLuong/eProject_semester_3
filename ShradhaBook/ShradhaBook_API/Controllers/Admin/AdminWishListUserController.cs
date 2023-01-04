using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.WishListUserService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminWishListUserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IWishListUserService _wishListUserService;

    public AdminWishListUserController(IWishListUserService wishListUserService, IProductService productService,
        IMapper mapper)
    {
        _wishListUserService = wishListUserService;
        _productService = productService;
        _mapper = mapper;
    }

    // GET: api/WishListUserGets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WishListUserGet>>> GetWishListUser()
    {
        //try
        {
            var result = await _wishListUserService.GetAllWishListUserAsync();

            return Ok(new MyServiceResponse<List<WishListUserGet>>(result, true, ""));
        }
        //catch
        //{
        //    return StatusCode(500, new MyServiceResponse<List<WishListUserGet>>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));

        //}
    }

    // GET: api/WishListUserGets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WishListUserGet>> GetWishListUser(int id)
    {
        try
        {
            var result = await _wishListUserService.GetWishListUserAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<WishListUserGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<WishListUserGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<WishListUserGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // PUT: api/WishListUserGets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWishListUser(int id, WishListUserPost model)
    {
        try
        {
            var status = await _wishListUserService.UpdateWishListUserAsync(id, model);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<WishListUserGet>(false,
                    MyStatusCode.UPDATE_FAILURE_RESULT +
                    ", There is already a WishListUser of this WhishListId and this UserId"));

            if (status == MyStatusCode.NOTFOUND)
            {
                return BadRequest(new MyServiceResponse<WishListGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", Not found User or Wishlist"));
            }

            if (status == MyStatusCode.SUCCESS)
            {
                var entity = await _wishListUserService.GetWishListUserAsync(id);
                return Ok(new MyServiceResponse<WishListUserGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));
            }

            return BadRequest(new MyServiceResponse<WishListUserGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<WishListUserGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    [HttpGet("GetAllWishListUserByUserId{id}")]
    public async Task<ActionResult<WishListUserGet>> GetAllWishListUserByUserId(int id)
    {
        try
        {
            var result = await _wishListUserService.GetWishListUsersByUserIdAsync(id);

            return result == null
                ? NotFound(new MyServiceResponse<WishListUserGet>(false, MyStatusCode.NOT_FOUND_RESULT))
                : Ok(new MyServiceResponse<WishListUserGet>(result));
        }
        catch
        {
            return StatusCode(500,
                new MyServiceResponse<WishListUserGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
        }
    }

    // POST: api/WishListUserGets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WishListUserGet>> AddWishListUser(WishListUserPost model)
    {
        //try
        {
            var status = await _wishListUserService.AddWishListUserAsync(model);
            if (status == MyStatusCode.DUPLICATE)
                return BadRequest(new MyServiceResponse<WishListUserGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT +
                    ", There is already a WishListUser of this WhishListId and this UserId"));

            if (status == MyStatusCode.NOTFOUND)
            {
                return BadRequest(new MyServiceResponse<WishListGet>(false,
                    MyStatusCode.ADD_FAILURE_RESULT + ", Not found User or Wishlist"));
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

    // DELETE: api/WishListUserGets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWishListUser(int id)
    {
        try
        {
            var result = await _wishListUserService.DeleteWishListUserAsync(id);
            if (result == MyStatusCode.SUCCESS)
                return Ok(new MyServiceResponse<WishListUserGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
            return BadRequest(new MyServiceResponse<WishListUserGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT));
        }
        catch
        {
            return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
        }
    }

    //private bool WishListUserGetExists(int id)
    //{
    //    return _context.WishListUserGet.Any(e => e.Id == id);
    //}
}