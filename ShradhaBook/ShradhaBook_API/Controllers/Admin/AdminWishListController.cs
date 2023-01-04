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
using ShradhaBook_API.Services.WishListService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminWishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public AdminWishListController(IWishListService wishListService, IProductService productService, IMapper mapper)
        {
            _wishListService = wishListService;
            _productService = productService;
            _mapper = mapper;

        }

        // GET: api/WishListGets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishListGet>>> GetAllWishList()
        {
            try
            {
                var result = await _wishListService.GetAllWishListAsync();

                return Ok(new MyServiceResponse<List<WishListGet>>(result, true, ""));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<List<WishListGet>>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // GET: api/WishListGets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishListGet>> GetWishList(int id)
        {
            try
            {
                var result = await _wishListService.GetWishListAsync(id);

                return result == null ? NotFound(new MyServiceResponse<WishListGet>(false, MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<WishListGet>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        // PUT: api/WishListGets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWishList(int id, WishListPost model)
        {
            try
            {

                int status = await _wishListService.UpdateWishListAsync(id, model);
                if (status == MyStatusCode.DUPLICATE)
                {
                    return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT + ", There is already a WishList of this ProductId"));
                }
                else if (status == MyStatusCode.NOTFOUND)
                {
                    return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", Not found Product"));

                }
                else if (status == MyStatusCode.SUCCESS)
                {
                    var entity = await _wishListService.GetWishListAsync(id);
                    return Ok(new MyServiceResponse<WishListGet>(entity, true, MyStatusCode.UPDATE_SUCCESS_RESULT));

                }
                return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.UPDATE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));

            }
        }

        // POST: api/WishListGets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WishListGet>> AddWishList(WishListPost model)
        {
            try
            {
                var status = await _wishListService.AddWishListAsync(model);
                if (status == MyStatusCode.DUPLICATE)
                {
                    return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", There is already a WishList of this ProductId"));
                }
                else if (status == MyStatusCode.NOTFOUND)
                {
                    return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.ADD_FAILURE_RESULT + ", Not found Product"));

                }
                else if (status > 0)
                {

                    var newEntity = await _wishListService.GetWishListAsync(status);

                    return Ok(new MyServiceResponse<WishListGet>(_mapper.Map<WishListGet>(newEntity), true, MyStatusCode.ADD_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.ADD_FAILURE_RESULT));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListGet>(false, MyStatusCode.INTERN_SEVER_ERROR_RESULT));


            }
        }
        [HttpGet("GetWishListByProductId{id}")]
        public async Task<ActionResult<WishListGet>> GetWishListByProductId(int id)
        {
            try
            {
                var result = await _wishListService.GetWishListByProductIdAsync(id);

                return result == null ? NotFound(new MyServiceResponse<WishListGet>(false, Helpers.MyStatusCode.NOT_FOUND_RESULT)) : Ok(new MyServiceResponse<WishListGet>(result));

            }
            catch
            {
                return StatusCode(500, new MyServiceResponse<WishListGet>(false, Helpers.MyStatusCode.INTERN_SEVER_ERROR_RESULT));
            }
        }

        // DELETE: api/WishListGets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishListGet(int id)
        {
            try
            {
                var result = await _wishListService.DeleteWishListAsync(id);
                if(result  == MyStatusCode.EXISTSREFERENCE)
                {
                    return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT + ", " + "There are already WishListUser  of WishList"));
                }
                else if(result == MyStatusCode.SUCCESS)
                {
                    return Ok(new MyServiceResponse<CategoryModelGet>(true, MyStatusCode.DELLETE_SUCCESS_RESULT));
                }
                return BadRequest(new MyServiceResponse<WishListGet>(false, MyStatusCode.DELLETE_FAILURE_RESULT));
            }
            catch
            {
                return StatusCode(500, MyStatusCode.INTERN_SEVER_ERROR_RESULT);
            }
        }
    }
}
