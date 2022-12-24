using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ComboProductService
{
    public class ComboProductService : IComboProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ComboProductService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddComboProductAsync(ComboProductModel model)
        {

            ComboProduct newModel = _mapper.Map<ComboProduct>(model);
            _context.ComboProducts!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

        public async Task DeleteComboProductAsync(int id)
        {
            var model = _context.ComboProducts!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.ComboProducts!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ComboProductModel>> GetAllComboProductAsync()
        {
            var allModel = await _context.ComboProducts!.ToListAsync();
            return _mapper.Map<List<ComboProductModel>>(allModel);
        }



        public async Task<ComboProductModel> GetComboProductAsync(int id)
        {
            var model = await _context.ComboProducts!.FindAsync(id);
            return _mapper.Map<ComboProductModel>(model);
        }

    }
}
