using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ManufacturerService;

public interface IManufacturerService
{
    Task<object> GetAllManufacturersAsync(string? name, string? code, int pageSize = 20, int pageIndex = 1);
    Task<ManufacturerModelGet> GetManufacturerAsync(int id);
    Task<int> AddManufacturerAsync(ManufacturerModelPost model);
    Task DeleteManufacturerAsync(int id);
    Task<int> UpdateManufacturerAsync(int id, ManufacturerModelPost model);
    Task<List<ManufacturerModelGet>> GetAllManufacturersAsync();
}