using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ComboService
{
    public class ComboService : IComboService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ComboService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddComboAsync(ComboModel model)
        {
            if (_context.Combos.Any(c => c.Name == model.Name))
            {
                return MyStatusCode.DUPLICATE_NAME;
            }
            Combo newModel = _mapper.Map<Combo>(model);
            _context.Combos!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

        public async Task DeleteComboAsync(int id)
        {
            var model = _context.Combos!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.Combos!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ComboModel>> GetAllComboAsync(string? name, int status = 0, int sortBy = 0)
        {
            var allModel = await _context.Combos
              .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim()))
              .Where(m => m.Status == status)!.ToListAsync();
            return _mapper.Map<List<ComboModel>>(allModel);
        }

        public async Task<ComboModel> GetComboAsync(int id)
        {
            var model = await _context.Combos!.FindAsync(id);
            return _mapper.Map<ComboModel>(model);
        }

        public async Task<int> UpdateComboAsync(int id, ComboModel model)
        {
            if (id == model.Id)
            {
               
                if (_context.Combos.Any(c => c.Name == model.Name && c.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_NAME;
                }
                var updateModel = _mapper.Map<Combo>(model);
                _context.Combos.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }

   
    }
}
