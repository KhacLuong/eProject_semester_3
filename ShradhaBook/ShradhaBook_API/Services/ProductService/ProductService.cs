using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.CategotyService;
using ShradhaBook_API.Services.ManufacturerService;
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




        public async Task<int> AddProductAsync(ProductModelPost model)
        {

            var categoty = await _context.Categories.Where(m => m.Id == model.CategoryId).FirstOrDefaultAsync();
            var manufacturer = await _context.Manufacturers.Where(m => m.Id == model.ManufacturerId).FirstOrDefaultAsync();

            if (model.Code.Length != 7 || !Helpers.Helpers.IsValidCode(model.Code))
            {
                return MyStatusCode.FAILURE;
            }
            if (categoty==null || !(model.Code.Substring(0, 2).ToUpper().Equals(categoty.Code.Substring(0, 2).ToUpper())))
            {
                return MyStatusCode.FAILURE;
            }
            //if (manufacturer==null||!(model.Code.Substring(2, 3).ToUpper().Equals(manufacturer.Code.Substring(0, 3).ToUpper())))
            //{
            //    return MyStatusCode.FAILURE;
            //}
            if (model.Name.Trim().Length == 0)
            {
                return MyStatusCode.FAILURE;

            }
            if (model.CategoryId < 1 || model.ManufacturerId < 1 || model.AuthorId < 1
                || model.Price < 0 || model.Quantity < 0)
            {
                return MyStatusCode.FAILURE;
            }

            if (_context.Products.Any(c => c.Code.Trim() == model.Code.Trim()))
            {
                return MyStatusCode.DUPLICATE_CODE;
            }
            
            if (_context.Products.Any(c => c.Name.Trim() == model.Name.Trim()))
            {
                return MyStatusCode.DUPLICATE_NAME;
            }
            Product newProduct = _mapper.Map<Product>(model);
            newProduct.CreatedAt = DateTime.Now;
            newProduct.UpdatedAt = DateTime.Now;
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

     

        public async Task<Object> GetAllProductAsync(string? name, string? code, string? status, int? categoryId, int? AuthorId, int? manufactuerId,
            decimal? lowPrice, decimal? hightPrice, long? lowQuantity, long? hightQuantity, int? sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {


            List<Product>? allModel;
            if (status != null && (status.Equals(MyStatus.ACTIVE_RESULT) || status.Equals(MyStatus.INACTIVE_RESULT)))
            {
                allModel = await _context.Products
            .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
            && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
            //.Where(m => (categoryId != 0) ? m.CategoryId == categoryId : m.CategoryId > 0)
             //.Where(m => (manufactoryId != 0) ? m.ManufacturerId == manufactoryId : m.ManufacturerId != manufactoryId)
            //.Where(m => (price != 0) ? m.Price == price : m.Price != price)
            .Where(m => m.Status == MyStatus.changeStatusCat(status))
            !.ToListAsync();
            }
            else
            {
                allModel = await _context.Products
            .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
            && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
            //.Where(m => (categoryId != 0) ? m.CategoryId == categoryId : m.CategoryId > 0)
             //.Where(m => (manufactoryId != 0) ? m.ManufacturerId == manufactoryId : m.ManufacturerId != manufactoryId)
             //.Where(m => (price != 0) ? m.Price == price : m.Price != price)
             !.ToListAsync();
            }

            var models = PaginatedList<Product>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<Product>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<ProductModelGet>>(models);
            return new
            {
                Products = result,
                totalPage = totalPage
            };
        }


        public async Task<ProductModelGet> GetProductAsync(int id)
        {
            var model = await _context.Products!.FindAsync(id);
            //var model = await _context.Products.Where(m => m.Id == id).Include(m.);


            return _mapper.Map<ProductModelGet>(model);
        }

        public async Task<int> UpdateProductAsync(int id, ProductModelPost model)
        {
            if (id == model.Id)
            {
                var categoty = (Category)_context.Categories.Where(m => m.Id == model.CategoryId);
                var manufacturer = (Manufacturer)_context.Manufacturers.Where(m => m.Id == model.ManufacturerId);

                if (model.Code.Length != 7 || !Helpers.Helpers.IsValidCode(model.Code))
                {
                    return MyStatusCode.FAILURE;
                }
                if (!model.Code.Substring(0, 2).Equals(categoty.Code.Substring(0, 2)))
                {
                    return MyStatusCode.FAILURE;
                }
                if (!model.Code.Substring(2, 3).Equals(manufacturer.Code.Substring(0, 3)))
                {
                    return MyStatusCode.FAILURE;
                }
                if (model.Name.Trim().Length == 0)
                {
                    return MyStatusCode.FAILURE;

                }
                if (model.CategoryId < 1 || model.ManufacturerId < 1 || model.AuthorId < 1
                    || model.Price < 0 || model.Quantity < 0)
                {
                    return MyStatusCode.FAILURE;
                }

                if (_context.Products.Any(c => c.Code.Trim() == model.Code.Trim()&&c.Id!=model.Id))
                {
                    return MyStatusCode.DUPLICATE_CODE;
                }
                if (_context.Products.Any(c => c.Name.Trim() == model.Name.Trim()&& c.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_NAME;
                }
                var updateModel = _mapper.Map<Product>(model);
                updateModel.UpdatedAt = DateTime.Now;
                _context.Products.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }

        public async Task<List<ProductModelGet>> GetProductByIdCategoryAsync(int categoryId)
        {

            var result = await _context.Products
              .Where(m => m.CategoryId == categoryId).Include(m => m.ManufacturerId)
              !.ToListAsync();
            return _mapper.Map<List<ProductModelGet>>(result);
        }



        public async Task<List<ProductModelGet>> GetProductByIdAuthorAsync(int authorId)
        {
            var result = await _context.Products
              .Where(m => m.AuthorId == authorId)
              !.ToListAsync();
            return _mapper.Map<List<ProductModelGet>>(result);
        }

        public async  Task<List<ProductModelGet>> GetProductByIdManufactuerAsync(int manufacturerId)
        {
            var result = await _context.Products
             .Where(m => m.ManufacturerId == manufacturerId)
             !.ToListAsync();
            return _mapper.Map<List<ProductModelGet>>(result);
        }
    }
}
