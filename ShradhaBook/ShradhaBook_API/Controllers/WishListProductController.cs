using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Data;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.WishListProductService;
using ShradhaBook_API.Services.WishListUserService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListProductController : ControllerBase
    {
        private readonly IWishListProductService _wishListProductService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public WishListProductController(IWishListProductService wishListProductService, IProductService productService, IMapper mapper)
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


        // POST: api/WishListProduct
        [HttpPost]
        public async Task<ActionResult<WishListProductPost>> AddWishListProductAsync(int userId, int prouctId)
        {
            try
            {
                var status = await _wishListProductService.AddWishListProductAsync(userId, prouctId);
                if (status == MyStatusCode.DUPLICATE)
                {
                    return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", product already exists in wishlist "));
                }

                else if (status > 0)
                {


                    return Ok(new MyServiceResponse<WishListProductGet>( true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.ADD_FAILURE_RESULT));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListProductGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


            }
        }

        // DELETE: api/WishListUser/5
        [HttpDelete]
        public async Task<IActionResult> DeleteWishListUserGet(int userId, int prouctId)
        {
            try
            {
                var status = await _wishListProductService.DeleteWishListProductAsync(userId, prouctId);


                if (status == MyStatusCode.SUCCESS)
                {
                    return Ok(new MyServiceResponse<WishListProductGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));

                }
                return BadRequest(new MyServiceResponse<WishListProductGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListProductGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        [HttpGet("GetTotalWishListAndCart{userId}")]
        public async Task<ActionResult<Object>> GetTotalWishListAndCart(int userId)
        {
            try
            {
                var result = await _wishListProductService.GetCountWishListAndCart(userId);

                return result == null ? NotFound(new MyServiceResponse<Object>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<Object>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<Object>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

    }
}
