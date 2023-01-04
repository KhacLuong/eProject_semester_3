using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.BlogTagService; 

public class BlogTagService : IBlogTagService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BlogTagService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddBlogTagAsync(BlogTagModelPost model)
    {
        if (_context.BlogTags.Any(c => c.BlogId == model.BlogId && c.TagId == model.TagId))
            return MyStatusCode.DUPLICATE;
        if (!_context.Blogs.Any(p => p.Id == model.BlogId) || !_context.Tags.Any(t => t.Id == model.TagId))
            return MyStatusCode.NOTFOUND;


        var newModel = _mapper.Map<BlogTag>(model);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.BlogTags!.Add(newModel);
        await _context.SaveChangesAsync();
        return newModel.Id;
    }

    public async Task DeleteBlogTagAsync(int id)
    {
        var model = _context.BlogTags!.SingleOrDefault(c => c.Id == id);
        if (model != null)
        {
            _context.BlogTags!.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<object> GetAllBlogTagAsync(string? blogTitle, string? tagName, int pageSize = 20,
        int pageIndex = 1)
    {
        IEnumerable<BlogTagModelGet>? query = null;


        query = await (from BT in _context.BlogTags
            join B in _context.Blogs
                on BT.BlogId equals B.Id
            join T in _context.Tags
                on BT.TagId equals T.Id
            select new BlogTagModelGet(BT.Id, B.Title, T.Name, BT.CreatedAt, BT.UpdatedAt)).ToListAsync();

        if (blogTitle != null && blogTitle.Trim().Length != 0)
            query = query.Where(m => m.BlogTitle != null && m.BlogTitle.Contains(blogTitle));
        if (tagName != null && tagName.Trim().Length != 0)
            query = query.Where(m => m.TagName != null && m.TagName.Contains(tagName));
        var allModel = query.ToList();
        var models = PaginatedList<BlogTagModelGet>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<BlogTagModelGet>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<BlogTagModelGet>>(models);
        return new
        {
            Manufacturers = result, totalPage
        };
    }

    public async Task<BlogTagModelGet> GetBlogTagAsync(int id)
    {
        var model = await (from BT in _context.BlogTags.Where(m => m.Id == id)
            join B in _context.Blogs
                on BT.BlogId equals B.Id
            join T in _context.Tags
                on BT.TagId equals T.Id
            select new BlogTagModelGet(BT.Id, B.Title, T.Name, BT.CreatedAt, BT.UpdatedAt)).ToListAsync();
        if (model == null || model.Count == 0) return null;
        return model[0];
    }

    public async Task<int> UpdateBlogTagAsync(int id, BlogTagModelPost model)
    {
        if (id == model.Id)
        {
            if (_context.BlogTags.Any(c => c.BlogId == model.BlogId && c.TagId == model.TagId))
                return MyStatusCode.DUPLICATE;
            if (!_context.Blogs.Any(p => p.Id == model.BlogId) || !_context.Tags.Any(t => t.Id == model.TagId))
                return MyStatusCode.NOTFOUND;

            var updateModel = _mapper.Map<BlogTag>(model);
            updateModel.UpdatedAt = DateTime.Now;
            _context.BlogTags.Update(updateModel);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }
}