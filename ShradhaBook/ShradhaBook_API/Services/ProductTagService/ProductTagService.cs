using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.ProductTagService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductTagService
{
    public class ProductTagService : IProductTagService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ITagService  _tagService;
        public ProductTagService(DataContext context, IMapper mapper, IProductService productService, ITagService tagService)
        {
            this._context = context;
            this._mapper = mapper;
            _productService = productService;
            _tagService = tagService;
        }

        public async Task<int> AddProductTagAsync(ProductTagPost model)
        {
            if (_context.ProductTags.Any(c => c.ProductId == model.ProductId && c.TagId==model.TagId))
            {
                return MyStatusCode.DUPLICATE;
            }
            if(!( _context.Products.Any(p=>p.Id == model.ProductId))||!(_context.Tags.Any(t=>t.Id==model.ProductId))){
                return MyStatusCode.NOTFOUND;
            }

            
            var newModel = _mapper.Map<ProductTag>(model);
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = null;
            _context.ProductTags!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

        public async  Task DeleteProductTagAsync(int id)
        {
            var model = _context.ProductTags!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.ProductTags!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<object> GetAllProductTagAsync( string? productName, string? tagName, int pageSize = 20, int pageIndex = 1)
        {

            IEnumerable<ProductTagGet>? query = null;

       

            query = await (from PT in _context.ProductTags
                           join P in _context.Products
                                    on PT.ProductId equals P.Id
                           join T in _context.Tags
                           on PT.TagId equals T.Id
                           select new ProductTagGet(PT.Id, P.Name,T.Name, PT.CreatedAt, PT.UpdatedAt)).ToListAsync();

            if (productName != null && productName.Trim().Length != 0)
            {
                query = query.Where(m => m.ProductName != null && m.ProductName.Contains(productName));
            }
            if(tagName != null && tagName.Trim().Length != 0)
            {
                query = query.Where(m => m.TagName != null && m.TagName.Contains(tagName));
            }
            var allModel = query.ToList();
            var models = PaginatedList<ProductTagGet>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<ProductTagGet>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<ProductTagGet>>(models);
            return new
            {
                Manufacturers = result,
                totalPage = totalPage
            };
        }

        public async  Task<ProductTagGet> GetProductTagAsync(int id)
        {
            var model = await (from PT in _context.ProductTags.Where(m => m.Id == id)
                               join P in _context.Products
                                   on PT.ProductId equals P.Id
                          join T in _context.Tags
                          on PT.TagId equals T.Id
                          select new ProductTagGet(PT.Id, P.Name, T.Name, PT.CreatedAt, PT.UpdatedAt)).ToListAsync();
            if (model == null || model.Count == 0)
            {
                return null;
            }
            return model[0];
            
        }

        public async Task<int> UpdateProductTagAsync(int id, ProductTagPost model)
        {
            if (id == model.Id)
            {
                if (_context.ProductTags.Any(c => c.ProductId == model.ProductId && c.TagId == model.TagId&&c.Id!=model.Id))
                {
                    return MyStatusCode.DUPLICATE;
                }
                if (_context.ProductTags.Any(c => c.ProductId == model.ProductId && c.TagId == model.TagId))
                {
                    return MyStatusCode.DUPLICATE;
                }
                if (!(_context.Products.Any(p => p.Id == model.ProductId)) || !(_context.Tags.Any(t => t.Id == model.ProductId)))
                {
                    return MyStatusCode.NOTFOUND;
                }
                var updateModel = _mapper.Map<ProductTag>(model);
                updateModel.UpdatedAt = DateTime.Now;
                _context.ProductTags.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
