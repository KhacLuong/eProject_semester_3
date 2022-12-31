namespace ShradhaBook_API.Services.StorageService;

public interface IStorageService
{
    string? UploadAvatar(IFormFile formFile, string email);
    string? UploadProductImage(IFormFile formFile, string productSlug);
}