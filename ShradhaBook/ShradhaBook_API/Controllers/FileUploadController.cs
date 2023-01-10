using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IStorageService _storageService;

    public FileUploadController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    /// <summary>
    ///     Upload user avatar given user email
    /// </summary>
    /// <param name="file"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost("avatar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UploadAvatar(IFormFile file, string email)
    {
        var message = _storageService.UploadAvatar(file, email);
        if (message == null)
            return NotFound(new ServiceResponse<object> { Status = false, Message = "User not found." });

        return Ok(new ServiceResponse<string> { Message = "Avatar file has been uploaded" });
    }

    /// <summary>
    ///     Upload product image given product slug
    /// </summary>
    /// <param name="file"></param>
    /// <param name="productSlug"></param>
    /// <returns></returns>
    [HttpPost("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UploadProducts(IFormFile file, string productSlug)
    {
        var message = _storageService.UploadProductImage(file, productSlug);
        if (message == null)
            return NotFound(new ServiceResponse<object> { Status = false, Message = "Product not found." });

        return Ok(new ServiceResponse<string> { Message = "Product file has been uploaded" });
    }
}