using System.Security.Cryptography;
using Azure.Storage.Blobs;

namespace ShradhaBook_API.Services.StorageService;

public class StorageService : IStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public StorageService(
        BlobServiceClient blobServiceClient,
        IConfiguration configuration,
        DataContext context)
    {
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
        _context = context;
    }

    public string? UploadAvatar(IFormFile formFile, string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return null;
        var userInfo = _context.UserInfo.FirstOrDefault(ui => ui.UserId == user.Id);
        if (userInfo == null) return null;
        var containerName = _configuration.GetSection("Storage:AvatarContainerName").Value;

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var fileName = CreateRandomFileName();
        var blobClient = containerClient.GetBlobClient(fileName);

        using var stream = formFile.OpenReadStream();
        blobClient.Upload(stream, true);
        userInfo.Avatar = fileName;
        _context.SaveChanges();
        return "ok";
    }

    public string? UploadProductImage(IFormFile formFile, string productSlug)
    {
        var product = _context.Products.FirstOrDefault(p => p.Slug == productSlug);
        if (product == null) return null;
        var containerName = _configuration.GetSection("Storage:ProductContainerName").Value;

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var fileName = CreateRandomFileName();
        var blobClient = containerClient.GetBlobClient(fileName);

        using var stream = formFile.OpenReadStream();
        blobClient.Upload(stream, true);
        product.ImageProductPath = fileName;
        product.ImageProductName = fileName[..16];
        _context.SaveChanges();
        return "OK";
    }

    private string CreateRandomFileName()
    {
        var fileName = Convert.ToHexString(RandomNumberGenerator.GetBytes(8)) + ".png";
        if (_context.UserInfo.Any(ui => ui.Avatar == fileName)
            || _context.Products.Any(p => p.ImageProductPath == fileName))
            fileName = CreateRandomFileName();
        return fileName;
    }
}