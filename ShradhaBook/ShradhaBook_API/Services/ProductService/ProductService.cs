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
                return Helpers.MyStatusCode.FAILURE;

            }
            if (model.CategoryId < 1 || model.ManufacturerId < 1 || model.AuthorId < 1
                || model.Price < 0 || model.Quantity < 0)
            {
                return Helpers.MyStatusCode.FAILURE;
            }

            if (_context.Products.Any(c => c.Code.Trim() == model.Code.Trim()))
            {
                return Helpers.MyStatusCode.DUPLICATE_CODE;
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

            List<ProductModelGet>? allModel;


            if (status != null && (status.Equals(MyStatus.ACTIVE_RESULT) || status.Equals(MyStatus.INACTIVE_RESULT)))
            {
               allModel = await (from P in _context.Products
                            .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
                            && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
                            //.Where(m => (categoryId != 0) ? m.CategoryId == categoryId : m.CategoryId > 0)
                            //.Where(m => (manufactoryId != 0) ? m.ManufacturerId == manufactoryId : m.ManufacturerId != manufactoryId)
                            //.Where(m => (price != 0) ? m.Price == price : m.Price != price)
                            .Where(m => m.Status == MyStatus.changeStatusCat(status))
                                join M in _context.Manufacturers
                                on P.ManufacturerId equals M.Id
                                join A in _context.Authors
                                on P.AuthorId equals A.Id
                                join C in _context.Categories
                                on P.CategoryId equals C.Id

                                select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug, P.CreatedAt, P.UpdatedAt)).ToListAsync();
            }
            else
            {
                allModel = await  (from P in  _context.Products
                            .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
                            && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
                            //.Where(m => (categoryId != 0) ? m.CategoryId == categoryId : m.CategoryId > 0)
                            //.Where(m => (manufactoryId != 0) ? m.ManufacturerId == manufactoryId : m.ManufacturerId != manufactoryId)
                            //.Where(m => (price != 0) ? m.Price == price : m.Price != price)
                            join M in _context.Manufacturers
                            on P.ManufacturerId equals M.Id
                            join A in _context.Authors
                            on P.AuthorId equals A.Id
                            join C in _context.Categories
                            on P.CategoryId equals C.Id
                            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                        P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug, P.CreatedAt, P.UpdatedAt)).ToListAsync();
            }



        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<ProductModelGet>>(models);
            return new
            {
                Products = result,
                totalPage = totalPage
            };
        }


        public async Task<object> GetProductDetailAsync(int id)
        {
            //var model = await _context.Products!.FindAsync(id);

            //return _mapper.Map<ProductModelGet>(model);
            //var model = await _context.Products.Include(p => p.Manufacturer).Where(m => m.Id == id).FirstOrDefaultAsync()

            var model = await (from P in _context.Products.Where(m=>m.Id ==id)
                        join M in _context.Manufacturers
                        on P.ManufacturerId equals M.Id
                        join A in _context.Authors
                        on P.AuthorId equals A.Id
                        join C in _context.Categories
                        on P.CategoryId equals C.Id
                        select new ProductDetail (P.Id,P.Code,P.Name, _mapper.Map<CategoryModelGet>(C), P.Price,P.Quantity, _mapper.Map<ManufacturerModelGet>(M), _mapper.Map<AuthorModelGet>(A), P.Description,
                        P.Intro,P.ImageProductPath,P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,P.CreatedAt,P.UpdatedAt)).ToListAsync();
                         
            return model[0];

        }
        public async Task<ProductModelGet> GetProductAsync(int id)
        {
          

            var model = await (from P in _context.Products.Where(p=>p.Id==id)
                         
                                  //.Where(m => (categoryId != 0) ? m.CategoryId == categoryId : m.CategoryId > 0)
                                  //.Where(m => (manufactoryId != 0) ? m.ManufacturerId == manufactoryId : m.ManufacturerId != manufactoryId)
                                  //.Where(m => (price != 0) ? m.Price == price : m.Price != price)
                              join M in _context.Manufacturers
                              on P.ManufacturerId equals M.Id
                              join A in _context.Authors
                              on P.AuthorId equals A.Id
                              join C in _context.Categories
                              on P.CategoryId equals C.Id
                              select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                          P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug, P.CreatedAt, P.UpdatedAt)).ToListAsync();
            var result = model[0];

            return _mapper.Map<ProductModelGet>(result);

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

        public async Task<Object> GetProductByIdCategoryAsync(int categoryId,int pageSize = 20, int pageIndex = 1)
        {

           
           var allModel = await (from P in _context.Products.Where(p => p.CategoryId == categoryId) 
                              join M in _context.Manufacturers
                              on P.ManufacturerId equals M.Id
                              join A in _context.Authors
                              on P.AuthorId equals A.Id
                              join C in _context.Categories
                              on P.CategoryId equals C.Id
                              select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                          P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug, P.CreatedAt, P.UpdatedAt)).ToListAsync();
      
            var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<ProductModelGet>>(models);
            return new
            {
                Products = result,
                totalPage = totalPage
            };
        }



        public async Task<Object> GetProductByIdAuthorAsync(int authorId, int pageSize = 20, int pageIndex = 1)
        {
            var allModel = await (from P in _context.Products.Where(p => p.AuthorId == authorId)
                                  join M in _context.Manufacturers
                                  on P.ManufacturerId equals M.Id
                                  join A in _context.Authors
                                  on P.AuthorId equals A.Id
                                  join C in _context.Categories
                                  on P.CategoryId equals C.Id
                                  select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                              P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug, P.CreatedAt, P.UpdatedAt)).ToListAsync();

            var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<ProductModelGet>>(models);
            return new
            {
                Products = result,
                totalPage = totalPage
            };
        }

        public async  Task<Object> GetProductByIdManufactuerAsync(int manufacturerId, int pageSize = 20, int pageIndex = 1)
        {
            var allModel = await (from P in _context.Products.Where(p => p.ManufacturerId == manufacturerId)
                                  join M in _context.Manufacturers
                                  on P.ManufacturerId equals M.Id
                                  join A in _context.Authors
                                  on P.AuthorId equals A.Id
                                  join C in _context.Categories
                                  on P.CategoryId equals C.Id
                                  select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                              P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug, P.CreatedAt, P.UpdatedAt)).ToListAsync();

            var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<ProductModelGet>>(models);
            return new
            {
                Products = result,
                totalPage = totalPage
            };
        }

        public  async Task<bool> checkExistProductByIdCategoryAsync(int categoryId)
        {
            var allModel =  await  _context.Products.Where(p => p.Id == categoryId).ToListAsync();
            if (allModel != null && allModel.Count!=0 )
            {
                return true;
            }
            return false;
        }



        public async  Task<bool> checkExistProductByIdAuthorAsync(int authorId)
        {
            var allModel = await _context.Products.Where(p => p.Id == authorId).ToListAsync();
            if (allModel != null && allModel.Count != 0)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> checkExistProductByIdManufactuerAsync(int manufacturerId)
        {
            var allModel = await _context.Products.Where(p => p.Id == manufacturerId).ToListAsync();
            if (allModel != null && allModel.Count != 0)
            {
                return true;
            }
            return false;
        }
    }
}
