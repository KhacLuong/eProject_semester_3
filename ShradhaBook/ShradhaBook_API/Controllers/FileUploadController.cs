using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public FileUploadController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        //public IActionResult Get()
        //{
        //    return Ok("File Upload API running...");
        //}

        [HttpPost("avatar")]
        public IActionResult UploadAvatar(IFormFile file, string email)
        {
            _storageService.UploadAvatar(file, email);

            return Ok(new ServiceResponse<string> { Message = "Avatar file has been uploaded" });
        }

        [HttpPost("products")]
        public IActionResult UploadProducts(IFormFile file, string productSlug)
        {
            _storageService.UploadProductImage(file, productSlug);

            return Ok(new ServiceResponse<string> { Message = "Product file has been uploaded" });
        }
    }
}
