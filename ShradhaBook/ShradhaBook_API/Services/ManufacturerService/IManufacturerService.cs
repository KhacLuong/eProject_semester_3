using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task<List<ManufacturerModel>> GetAllManufacturersAsync(string name, string code);
        Task<ManufacturerModel> GetManufacturerAsync(int id);
        Task<int> AddManufacturerAsync(ManufacturerModel model);
        Task DeleteManufacturerAsync(int id);
        Task<int> UpdateManufacturerAsync(int id, ManufacturerModel model);

    }
}
