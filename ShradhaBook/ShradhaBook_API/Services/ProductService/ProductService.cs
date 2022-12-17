using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }



        public async Task<int> AddProductAsync(ProductModel model)
        {


            if (_context.Products.Any(c => c.Code == model.Code))
            {
                return MyStatusCode.DUPLICATE_CODE;
            }
            if (_context.Products.Any(c => c.Name == model.Name))
            {
                return MyStatusCode.DUPLICATE_NAME;
            }

            Product newProduct = _mapper.Map<Product>(model);
            _context.Products!.Add(newProduct);
            await _context.SaveChangesAsync();
            if(await _context.Products!.FindAsync(newProduct.Id) == null)
            {
                return MyStatusCode.FAILURE;
            }
            return newProduct.Id;
        }


        public async Task DeleteProductAsync(int id)
        {
            var result = _context.Products!.SingleOrDefault(c => c.Id == id);
            if (result != null)
            {
                _context.Products!.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

     

        public async Task<List<ProductModel>> GetAllProductAsync(string? name, string? code, int? categoryId, int? manufactoryId, decimal? price, long quantity, int? status = 0, int? sortBy = 0)
        {

            var allProducts = await _context.Products
              .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
              && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
              //.Where(m=> (categoryId != 0) ? m.CategoryId==categoryId:m.CategoryId!=categoryId)
              //.Where(m => (manufactoryId != 0) ? m.ManufacturerId == manufactoryId : m.ManufacturerId != manufactoryId)
              //.Where(m => (price != 0) ? m.Price == price : m.Price != price)
              //.Where(m => m.Status == status)
              !.ToListAsync();
            return _mapper.Map<List<ProductModel>>(allProducts);
        }

      

        public Task<object?> GetAllProductAsync(string? name, string? code, int categoryId, int? manufactoryId, long quantity, decimal? price, int? status, int? sortBy)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var category = await _context.Products!.FindAsync(id);
            return _mapper.Map<ProductModel>(category);
        }

        public async Task<int> UpdateProductAsync(int id, ProductModel model)
        {
            if (id == model.Id)
            {
                if (_context.Products.Any(c => c.Code == model.Code&&c.Id!=model.Id))
                {
                    return MyStatusCode.DUPLICATE_CODE;
                }
                if (_context.Products.Any(c => c.Name == model.Name&& c.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_NAME;
                }
                var updateProduct = _mapper.Map<Product>(model);
                _context.Products.Update(updateProduct);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
