using Azure.Storage.Blobs;

namespace ShradhaBook_API.Services.StorageService;

public class StorageService : IStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;

    public StorageService(
        BlobServiceClient blobServiceClient,
        IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
    }

    public void UploadAvatar(IFormFile formFile, string email)
    {
        var containerName = _configuration.GetSection("Storage:AvatarContainerName").Value;

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(email + ".png");

        using var stream = formFile.OpenReadStream();
        blobClient.Upload(stream, true);
    }

    public void UploadProductImage(IFormFile formFile, string productSlug)
    {
        var containerName = _configuration.GetSection("Storage:ProductContainerName").Value;

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(productSlug + ".png");

        using var stream = formFile.OpenReadStream();
        blobClient.Upload(stream, true);
    }
}