namespace ShradhaBook_API.Services.RateService;

public interface IRateService
{
    Task<int> AddRateAsync(RateModelPost model);
    Task DeleteRateAsync(int id);
    Task<int> UpdateRateAsync(int id, RateModelPost model);
    Task<object> GetRatesByProductIdAsync(int productId, int pageSize = 20, int pageIndex = 1);
    Task<RateModelGet> GetRateById(int id);
    Task<object> GetRatesAndCommentByProductIdAsync(int productId, int pageSize = 20, int pageIndex = 1);
}