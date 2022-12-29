namespace ShradhaBook_API.Services.StorageService
{
    public interface IStorageService
    {
        void UploadAvatar(IFormFile formFile, string email);
        void UploadProductImage(IFormFile formFile, string productSlug);
    }
}
