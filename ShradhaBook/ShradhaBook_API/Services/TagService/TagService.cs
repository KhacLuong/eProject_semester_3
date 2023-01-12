using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.TagService;

public class TagService : ITagService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TagService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddTagAsync(TagModelPost model)
    {
        if (_context.Tags.Any(c => c.Name == model.Name)) return MyStatusCode.DUPLICATE_NAME;
        var newModel = _mapper.Map<Tag>(model);
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

    public async Task<object> GetAllTagAsync(string? name, int sortBy = 0, int pageSize = 20, int pageIndex = 1)
    {
        var allModel = await _context.Tags
            .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim()))
            !.ToListAsync();

        var models = PaginatedList<Tag>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<Tag>.totlalPage(allModel, pageSize);

        var result = _mapper.Map<List<TagModelGet>>(models);
        return new
        {
            Tags = result, totalPage
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
            if (_context.Tags.Any(c => c.Name == model.Name && c.Id != model.Id)) return MyStatusCode.DUPLICATE_NAME;
            var updateModel = _mapper.Map<Tag>(model);

            updateModel.UpdatedAt = DateTime.Now;
            _context.Tags.Update(updateModel);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }

    public async Task<object> GetTagsByProductId(int id, int pageSize = 20, int pageIndex = 1)
    {
        var allModel = await (from P in _context.Products.Where(p => p.Id == id)
            join PT in _context.ProductTags
                on P.Id equals PT.ProductId
            join T in _context.Tags
                on PT.TagId equals T.Id
            select new TagModelPost(T.Id, T.Name, T.CreatedAt, T.UpdatedAt)).ToArrayAsync();


        var listModel = _mapper.Map<List<Tag>>(allModel);
        var listGet = _mapper.Map<List<TagModelGet>>(listModel);
        var models = PaginatedList<TagModelGet>.Create(listGet, pageIndex, pageSize);
        var totalPage = PaginatedList<TagModelGet>.totlalPage(listGet, pageSize);
        var result = _mapper.Map<List<TagModelGet>>(models);

        return new
        {
            Products = result, totalPage
        };
    }

    public async Task<object> GetTagsByBlogId(int id, int pageSize = 20, int pageIndex = 1)
    {
        var allModel = await (from B in _context.Blogs.Where(p => p.Id == id)
            join BT in _context.BlogTags
                on B.Id equals BT.BlogId
            join T in _context.Tags
                on BT.TagId equals T.Id
            select new TagModelPost(T.Id, T.Name, T.CreatedAt, T.UpdatedAt)).ToArrayAsync();


        var listModel = _mapper.Map<List<Tag>>(allModel);
        var listGet = _mapper.Map<List<TagModelGet>>(listModel);
        var models = PaginatedList<TagModelGet>.Create(listGet, pageIndex, pageSize);
        var totalPage = PaginatedList<TagModelGet>.totlalPage(listGet, pageSize);
        var result = _mapper.Map<List<TagModelGet>>(models);

        return new
        {
            Products = result, totalPage
        };
    }
}