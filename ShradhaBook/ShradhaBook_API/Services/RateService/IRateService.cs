namespace ShradhaBook_API.Services.RateService;

public interface IRateService
{
    //Task<RateModelGet> GetRateAsync(int id);
    Task<int> AddRateAsync(RateModelPost model);
    Task DeleteRateAsync(int id);
    Task<int> UpdateRateAsync(int id, RateModelPost model);
    Task<List<RateModelGet>> GetRatesByProductIdAsync();
}