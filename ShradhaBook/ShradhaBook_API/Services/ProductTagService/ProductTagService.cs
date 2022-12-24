using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Services.ProductTagService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductTagService
{
    public class ProductTagService : IProductTagService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductTagService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddProductTagAsync(ProductTagModel model)
        {

            ProductTag newModel = _mapper.Map<ProductTag>(model);
            _context.ProductTags!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

        public async Task DeleteProductTagAsync(int id)
        {
            var model = _context.ProductTags!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.ProductTags!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductTagModel>> GetAllProductTagAsync()
        {
            var allModel = await _context.ProductTags!.ToListAsync();
            return _mapper.Map<List<ProductTagModel>>(allModel);
        }

        public async Task<ProductTagModel> GetProductTagAsync(int id)
        {
            var model = await _context.ProductTags!.FindAsync(id);
            return _mapper.Map<ProductTagModel>(model);
        }

    }
}
