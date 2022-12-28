using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TagService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddTagAsync(TagModelPost model)
        {
            if (_context.Tags.Any(c => c.Name == model.Name))
            {
                return MyStatusCode.DUPLICATE_NAME;
            }
            Tag newModel = _mapper.Map<Tag>(model);
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = null;
            _context.Tags!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

       
        public async Task DeleteTagAsync(int id)
        {
            var model = _context.Tags!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.Tags!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Object> GetAllTagAsync(string? name, int sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {
            var allModel = await _context.Tags
              .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim()))
                !.ToListAsync();

            var models = PaginatedList<Tag>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<Tag>.totlalPage(allModel, pageSize);

            var result =  _mapper.Map<List<TagModelGet>>(models);
            return new
            {
                Tags = result,
                totalPage = totalPage
            };
        }

        public async Task<TagModelGet> GetTagAsync(int id)
        {
            var model = await _context.Tags!.FindAsync(id);
            return _mapper.Map<TagModelGet>(model);
        }

        public async Task<int> UpdateTagAsync(int id, TagModelPost model)
        {
            if (id == model.Id)
            {
                if (_context.Tags.Any(c => c.Name == model.Name && c.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_NAME;
                }
                var updateModel = _mapper.Map<Tag>(model);

                updateModel.UpdatedAt = DateTime.Now;
                _context.Tags.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }

    }
}
