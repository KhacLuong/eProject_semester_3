﻿using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductService
{
    public interface IProductService
    {
        Task<Object> GetAllProductAsync(string? name, string? code, string? status, string? categoryName, string? authorName, string? manufactuerName,
            decimal? moreThanPrice, decimal? lessThanPrice, long? moreThanQuantity, long? lessThanQuantity, int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductDetailAsync(int id);
        Task<ProductModelGet> GetProductAsync(int id);
        Task<ProductDetail> GetProductDetailAsync(string slug);
        Task<int> AddProductAsync(ProductModelPost model);
        Task DeleteProductAsync(int id);

    Task<int> UpdateProductAsync(int id, ProductModelPost model);
    Task<bool> CheckExistProductByIdCategoryAsync(int categoryId);
    Task<bool> CheckExistProductByIdAuthorAsync(int authorId);
    Task<bool> CheckExistProductByIdManufactuerAsync(int manufacturerId);
    Task<bool> IncreaseViewCountProduct(int id);

        Task<Object> GetProductByIdCategoryAsync(int categoryId, int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductBySlugCategoryAsync(string categorySlug, int? sortBy = 0, int pageSize = 20, int pageIndex = 1);

        Task<Object> GetProductByIdAuthorAsync(int authorId, int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductByIdManufactuerAsync(int manufacturerId, int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductWishListByUserIdAsync(int id, int pageSize = 20, int pageIndex = 1);
        public List<ProductModelGet> SortProduct(IEnumerable<ProductModelGet> query, int? sortBy = 0);


    }
}
