using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ComboTagService
{
    public class ComboTagService : IComboTagService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ComboTagService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async  Task<int> AddComboTagAsync(ComboTagModel model)
        {

            ComboTag newModel = _mapper.Map<ComboTag>(model);
            _context.ComboTags!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

        public async Task DeleteComboTagAsync(int id)
        {
            var model = _context.ComboTags!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.ComboTags!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ComboTagModel>> GetAllComboTagAsync()
        {
            var allModel = await _context.ComboTags!.ToListAsync();
            return _mapper.Map<List<ComboTagModel>>(allModel);
        }

        public async Task<ComboTagModel> GetComboTagAsync(int id)
        {
            var model = await _context.ComboTags!.FindAsync(id);
            return _mapper.Map<ComboTagModel>(model);
        }

        public async Task<int> UpdateComboTagAsync(int id, ComboTagModel model)
        {
            if (id == model.Id)
            {

                var updateModel = _mapper.Map<ComboTag>(model);
                _context.ComboTags.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
