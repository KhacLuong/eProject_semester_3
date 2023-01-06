using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;

namespace ShradhaBook_API.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public ProductService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<int> AddProductAsync(ProductModelPost model)
    {
        //var productLast =  _context.Products.TakeLast(1).ToList();

        var categoty = await _context.Categories.Where(m => m.Id == model.CategoryId).FirstOrDefaultAsync();
        var manufacturer = await _context.Manufacturers.Where(m => m.Id == model.ManufacturerId).FirstOrDefaultAsync();

        //if (model.Code.Length != 7 || !Helpers.Helpers.IsValidCode(model.Code))
        //{
        //    return MyStatusCode.FAILURE;
        //}
        //if (categoty==null || !(model.Code.Substring(0, 2).ToUpper().Equals(categoty.Code.Substring(0, 2).ToUpper())))
        //{
        //    return MyStatusCode.FAILURE;
        //}
        ////if (manufacturer==null||!(model.Code.Substring(2, 3).ToUpper().Equals(manufacturer.Code.Substring(0, 3).ToUpper())))
        ////{
        ////    return MyStatusCode.FAILURE;
        ////}
        ///

        if (model.Name.Trim().Length == 0) return MyStatusCode.FAILURE;
        if (model.CategoryId < 1 || model.ManufacturerId < 1 || model.AuthorId < 1
            || model.Price < 0 || model.Quantity < 0)
            return MyStatusCode.FAILURE;

        if (_context.Products.Any(c => c.Name.Trim() == model.Name.Trim())) return MyStatusCode.DUPLICATE_NAME;
        //int a = 0;
        //string code ="";
        //if (productLast != null||productLast.Count!=0)
        //{
        //    a = productLast[0].Id;
        //}
        //if (a == 0)
        //{
        //   code = categoty.Code.Trim().Substring(0, 2).ToString() + categoty.Code.Trim().Substring(0, 2).ToString()+"01";
        //}else if (a < 10)
        //{
        //    code = categoty.Code.Trim().Substring(0, 2).ToString() + categoty.Code.Trim().Substring(0, 2).ToString() + "0"+a.ToString();
        //}
        //else
        //{
        //    code = categoty.Code.Trim().Substring(0, 2).ToString() + categoty.Code.Trim().Substring(0, 2).ToString()  + a.ToString();

        //}

        //model.Code = code.ToUpper();
        model.Code = "ABC";
        model.ViewCount = 0;
        model.Slug = Helpers.Helpers.Slugify(model.Name);
        var newProduct = _mapper.Map<Product>(model);
        newProduct.CreatedAt = DateTime.Now;
        newProduct.UpdatedAt = null;
        _context.Products!.Add(newProduct);
        await _context.SaveChangesAsync();
        if (await _context.Products!.FindAsync(newProduct.Id) == null) return MyStatusCode.FAILURE;
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


    public async Task<object> GetAllProductAsync(string? name, string? code, string? status, string? categoryName,
        string? authorName, string? manufactuerName,
        decimal? moreThanPrice, decimal? lessThanPrice, long? moreThanQuantity, long? lessThanQuantity, int? sortBy = 0,
        int pageSize = 20, int pageIndex = 1)
    {
        IEnumerable<ProductModelGet>? query = null;

        #region Fillter

        query = await (from P in _context.Products
                .Where(m => m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
                .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim()))
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount,
                P.CreatedAt,
                P.UpdatedAt)).ToListAsync();

        if (status != null && status.Trim().Length != 0 && (status.ToLower().Equals(MyStatus.ACTIVE_RESULT.ToLower()) ||
                                                            status.ToLower()
                                                                .Equals(MyStatus.INACTIVE_RESULT.ToLower())))
            query = query.Where(m => m.Status.ToLower().Equals(status.ToLower()));
        if (categoryName != null && categoryName.Trim().Length != 0)
            query = query.Where(m => m.Category != null && m.Category.Contains(categoryName));
        if (authorName != null && authorName.Trim().Length != 0)
            query = query.Where(m => m.Author.Contains(authorName));
        if (authorName != null && authorName.Trim().Length != 0)
            query = query.Where(m => m.Author.Contains(authorName));
        if (manufactuerName != null && manufactuerName.Trim().Length != 0)
            query = query.Where(m => m.Manufacturer.Contains(manufactuerName));
        if (lessThanPrice > 0) query = query.Where(m => m.Price <= lessThanPrice);
        if (moreThanPrice >= 0) query = query.Where(m => m.Price >= moreThanPrice);
        if (lessThanQuantity > 0) query = query.Where(m => m.Quantity <= lessThanQuantity);
        if (moreThanQuantity >= 0) query = query.Where(m => m.Quantity >= moreThanQuantity);

        #endregion Fillter


        #region Sort

        var allModel = SortProduct(query, sortBy);

        #endregion Sort


        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ProductModelGet>>(models);
        return new
        {
            Products = result, totalPage
        };
    }


    public async Task<object> GetProductDetailAsync(int id)
    {
        var model = await (from P in _context.Products.Where(m => m.Id == id)
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductDetail(P.Id, P.Code, P.Name, _mapper.Map<CategoryModelGet>(C), P.Price, P.Quantity,
                _mapper.Map<ManufacturerModelGet>(M), _mapper.Map<AuthorModelGet>(A), P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();

        if (model == null || model.Count == 0) return null;
        var result = model[0];
        return model[0];
    }

    public async Task<ProductModelGet> GetProductAsync(int id)
    {
        var model = await (from P in _context.Products.Where(p => p.Id == id)
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();

        if (model == null || model.Count == 0) return null;
        var result = model[0];
        return model[0];
    }

    public async Task<int> UpdateProductAsync(int id, ProductModelPost model)
    {
        if (id == model.Id)
        {
            var categoty = (Category)_context.Categories.Where(m => m.Id == model.CategoryId);
            var manufacturer = (Manufacturer)_context.Manufacturers.Where(m => m.Id == model.ManufacturerId);
            var code = categoty.Code.Trim().Substring(0, 2) + categoty.Code.Trim().Substring(0, 3);
            model.Code = code.ToUpper();
            //if (model.Code.Length != 7 || !Helpers.Helpers.IsValidCode(model.Code))
            //{
            //    return MyStatusCode.FAILURE;
            //}
            //if (!model.Code.Substring(0, 2).Equals(categoty.Code.Substring(0, 2)))
            //{
            //    return MyStatusCode.FAILURE;
            //}
            ////if (!model.Code.Substring(2, 3).Equals(manufacturer.Code.Substring(0, 3)))
            ////{
            ////    return MyStatusCode.FAILURE;
            ////}
            if (model.Name.Trim().Length == 0) return MyStatusCode.FAILURE;
            if (model.CategoryId < 1 || model.ManufacturerId < 1 || model.AuthorId < 1
                || model.Price < 0 || model.Quantity < 0)
                return MyStatusCode.FAILURE;

            if (_context.Products.Any(c => c.Name.Trim() == model.Name.Trim() && c.Id != model.Id))
                return MyStatusCode.DUPLICATE_NAME;
            model.Code = "ABC";
            model.Slug = Helpers.Helpers.Slugify(model.Name);

            var modelOld = await _context.Products.FindAsync(id);
            modelOld.Name = model.Name;
            modelOld.Code = model.Code;
            modelOld.Slug = model.Slug;
            modelOld.Description = model.Description;
            modelOld.CategoryId = model.CategoryId;
            modelOld.AuthorId = model.AuthorId;
            modelOld.ManufacturerId = model.ManufacturerId;
            modelOld.Price = model.Price;
            modelOld.Quantity = model.Quantity;
            modelOld.Intro = model.Intro;
            modelOld.ImageProductPath = model.ImageProductPath;
            modelOld.ImageProductName = model.ImageProductName;
            modelOld.ViewCount = model.ViewCount;
            modelOld.Slug = model.Slug;
            modelOld.Status = MyStatus.changeStatusCat(model.Status);
            modelOld.UpdatedAt = DateTime.Now;

            _context.Products.Update(modelOld);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }

    public async Task<object> GetProductByIdCategoryAsync(int categoryId, int? sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        IEnumerable<ProductModelGet>? query = null;
        query = await (from P in _context.Products.Where(p => p.CategoryId == categoryId)
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();

        var allModel = SortProduct(query, sortBy);
        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ProductModelGet>>(models);
        return new
        {
            Products = result,
            totalProduct = allModel.Count,
            totalPage
        };
    }


    public async Task<object> GetProductByIdAuthorAsync(int authorId, int? sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        IEnumerable<ProductModelGet>? query = null;

        query = await (from P in _context.Products.Where(p => p.AuthorId == authorId)
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();

        var allModel = SortProduct(query, sortBy);
        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ProductModelGet>>(models);
        return new
        {
            Products = result, totalPage
        };
    }

    public async Task<object> GetProductByIdManufactuerAsync(int manufacturerId, int? sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        IEnumerable<ProductModelGet>? query = null;
        query = await (from P in _context.Products.Where(p => p.ManufacturerId == manufacturerId)
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();


        var allModel = SortProduct(query, sortBy);
        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ProductModelGet>>(models);
        return new
        {
            Products = result, totalPage
        };
    }

    public async Task<bool> CheckExistProductByIdCategoryAsync(int categoryId)
    {
        var allModel = await _context.Products.Where(p => p.Id == categoryId).ToListAsync();
        if (allModel != null && allModel.Count != 0) return true;
        return false;
    }


    public async Task<bool> CheckExistProductByIdAuthorAsync(int authorId)
    {
        var allModel = await _context.Products.Where(p => p.Id == authorId).ToListAsync();
        if (allModel != null && allModel.Count != 0) return true;
        return false;
    }


    public async Task<bool> CheckExistProductByIdManufactuerAsync(int manufacturerId)
    {
        var allModel = await _context.Products.Where(p => p.Id == manufacturerId).ToListAsync();
        if (allModel != null && allModel.Count != 0) return true;
        return false;
    }

    public async Task<bool> IncreaseViewCountProduct(int id)
    {
        var model = await _context.Products.FindAsync(id);
        if (model != null)
        {
            model.ViewCount++;
            _context.Products.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }


    public List<ProductModelGet> SortProduct(IEnumerable<ProductModelGet> query, int? sortBy = 0)
    {
        List<ProductModelGet>? result = null;
        switch (sortBy)
        {
            case 1:
                query = query.OrderBy(m => m.Name);
                break;

            case 2:
                query = query.OrderByDescending(m => m.ViewCount);
                break;

            case 3:
                query = query.OrderBy(m => m.Price);
                break;

            case 4:
                query = query.OrderByDescending(m => m.Price);
                break;
            case 5:
                query = query.OrderByDescending(m => m.CreatedAt);
                break;
            default:
                query = query.OrderBy(m => m.Id);
                break;
        }

        result = query.ToList();
        return result;
    }

    public async Task<object> GetProductWishListByUserIdAsync(int id, int pageSize = 20, int pageIndex = 1)
    {
        IEnumerable<Product>? query = null;
        query = await (from p in _context.Products
            from w in p.WishLists
            from wu in w.WishListUsers
            where wu.UserId == id
            select p).ToListAsync();


        IEnumerable<ProductModelGet>? queryModdelGet = null;


        queryModdelGet = (from P in query
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToList();
        var allModel = queryModdelGet.ToList();

        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ProductModelGet>>(models);
        return new
        {
            Products = result, totalPage
        };
    }

    public async Task<ProductDetail> GetProductDetailAsync(string slug)
    {
        var model = await (from P in _context.Products.Where(m => m.Slug == slug)
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories
                on P.CategoryId equals C.Id
            select new ProductDetail(P.Id, P.Code, P.Name, _mapper.Map<CategoryModelGet>(C), P.Price, P.Quantity,
                _mapper.Map<ManufacturerModelGet>(M), _mapper.Map<AuthorModelGet>(A), P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();

        if (model == null || model.Count == 0) return null;
        var result = model[0];
        return result;
    }

    public async Task<object> GetProductBySlugCategoryAsync(string categorySlug, int? sortBy = 0, int pageSize = 20,
        int pageIndex = 1)
    {
        IEnumerable<ProductModelGet>? query = null;
        query = await (from P in _context.Products
            join M in _context.Manufacturers
                on P.ManufacturerId equals M.Id
            join A in _context.Authors
                on P.AuthorId equals A.Id
            join C in _context.Categories.Where(c => c.Slug.Equals(categorySlug))
                on P.CategoryId equals C.Id
            select new ProductModelGet(P.Id, P.Code, P.Name, C.Name, P.Price, P.Quantity, M.Name, A.Name, P.Description,
                P.Intro, P.ImageProductPath, P.ImageProductName, MyStatus.changeStatusCat(P.Status), P.Slug,
                P.ViewCount, P.CreatedAt, P.UpdatedAt)).ToListAsync();

        var allModel = SortProduct(query, sortBy);
        var models = PaginatedList<ProductModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<ProductModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ProductModelGet>>(models);
        return new
        {
            Products = result,
            totalProduct = allModel.Count,
            totalPage
        };
    }
}