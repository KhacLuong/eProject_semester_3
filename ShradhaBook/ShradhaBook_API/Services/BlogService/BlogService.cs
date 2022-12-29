using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.BlogService
{
    public class BlogService : IBlogService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;




        public BlogService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddBlogAsync(BlogModelPost model)
        {
            var checkExistAuthor =  _context.Authors.Any(a=>a.Id ==model.AuthorId);
            var checkExistsBlog = _context.Blogs.Any(b => b.AuthorId == model.AuthorId);
            if (model.Title.Trim().Length == 0)
            {
                return MyStatusCode.FAILURE;
            }
            if (!checkExistAuthor)
            {
                return MyStatusCode.NOTFOUND;
            }
            if (checkExistsBlog)
            {
                return MyStatusCode.DUPLICATE;
            }
            model.ViewCount = 0;
            model.Slug = Helpers.Helpers.Slugify(model.Title);
            Blog newModel = _mapper.Map<Blog>(model);
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = null;
            _context.Blogs!.Add(newModel);
            await _context.SaveChangesAsync();
            if (await _context.Blogs!.FindAsync(newModel.Id) == null)
            {
                return MyStatusCode.FAILURE;
            }
            return newModel.Id;

        }

        public async  Task DeleteBlogAsync(int id)
        {
            var result = _context.Products!.SingleOrDefault(c => c.Id == id);
            if (result != null)
            {
                _context.Products!.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<object> GetAllBlogAsync(string? tiltle, string? authorName, string? status, int pageSize = 20, int pageIndex = 1)
        {
            IEnumerable<BlogModelGet>? query = null;


            query = await (from B in _context.Blogs
                          .Where(m => m.Title.ToLower().Contains(string.IsNullOrEmpty(tiltle) ? "" : tiltle.ToLower().Trim()))
                          join A in _context.Authors
                          on B.AuthorId equals A.Id
                         
                          select new BlogModelGet(B.Id, B.Title,B.Description,B.Content,B.avatar, A.Name, 
                          MyStatus.changeStatusCat(B.Status), B.Slug, B.ViewCount,
                          B.CreatedAt,B.UpdatedAt)).ToListAsync();
            
            if ((status != null && status.Trim().Length != 0) && (status.ToLower().Equals(MyStatus.ACTIVE_RESULT.ToLower()) || status.ToLower().Equals(MyStatus.INACTIVE_RESULT.ToLower())))
            {
                query = query.Where(m => m.Status.ToLower().Equals(status.ToLower()));
            }
            if ( authorName!= null && authorName.Trim().Length != 0)
            {
                query = query.Where(m => m.AuthorName != null && m.AuthorName.Contains(authorName));
            }

            var allModel = query.ToList();
            var models = PaginatedList<BlogModelGet>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<BlogModelGet>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<BlogModelGet>>(models);
            return new
            {
                Products = result,
                totalPage = totalPage
            };
        }

        public async Task<BlogModelGet> GetBlogAsync(int id)
        {
            var model = await (from B in _context.Blogs.Where(B => B.Id == id)
                              join A in _context.Authors
                              on B.AuthorId equals A.Id
                              select new BlogModelGet(B.Id, B.Title, B.Description, B.Content, B.avatar, A.Name,
                              MyStatus.changeStatusCat(B.Status), B.Slug, B.ViewCount,
                              B.CreatedAt, B.UpdatedAt)).ToListAsync();

            if (model == null || model.Count == 0)
            {
                return null;
            }
            var result = model[0];
            return model[0];
        }

        public async Task<object> GetBlogByAuthordIdAsync(int authorId, int pageSize = 20, int pageIndex = 1)
        {
            var model = await (from B in _context.Blogs.Where(b=>b.AuthorId == authorId)
                              join A in _context.Authors
                              on B.AuthorId equals A.Id
                              select new BlogModelGet(B.Id, B.Title, B.Description, B.Content, B.avatar, A.Name,
                              MyStatus.changeStatusCat(B.Status), B.Slug, B.ViewCount,
                              B.CreatedAt, B.UpdatedAt)).ToListAsync();

            if (model == null || model.Count == 0)
            {
                return null;
            }
            var result = model[0];
            return model[0];
        }

        public async Task<BlogModelDetail> GetBlogDetailAsync(int id)
        {
            var model = await (from B in _context.Blogs.Where(B=>B.Id==id)
                          join A in _context.Authors
                          on B.AuthorId equals A.Id
                          select new BlogModelDetail(B.Id, B.Title, B.Description, B.Content, B.avatar, A,
                          MyStatus.changeStatusCat(B.Status), B.Slug, B.ViewCount,
                          B.CreatedAt, B.UpdatedAt)).ToListAsync();

            if (model == null || model.Count == 0)
            {
                return null;
            }
            var result = model[0];
            return model[0];

        }

        public async  Task<bool> IncreseCountViewBlogAsync(int id)
        {
            var model = await _context.Blogs.FindAsync(id);
            if (model != null)
            {
                model.ViewCount++;
                _context.Blogs.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> UpdateBlogAsync(int id, BlogModelPost model)
        {
            if (id == model.Id)
            {
                var checkExistsAuthor = _context.Authors.Any(a=>a.Id == model.AuthorId);
                var checkExistsBlog = _context.Blogs.Any(b => b.AuthorId == model.AuthorId && b.Id!=model.Id);


                if (model.Title.Trim().Length == 0)
                {
                    return MyStatusCode.FAILURE;
                }
                if (!checkExistsAuthor)
                {
                    return MyStatusCode.NOTFOUND;
                }
                if (checkExistsBlog)
                {
                    return MyStatusCode.DUPLICATE;
                }

                model.Slug = Helpers.Helpers.Slugify(model.Title);

                var modelOld = await _context.Blogs.FindAsync(id);
                var createOld = modelOld.CreatedAt;
                modelOld = _mapper.Map<Blog>(model);
                modelOld.CreatedAt = createOld;

                modelOld.Status = MyStatus.changeStatusCat(model.Status);
                modelOld.UpdatedAt = DateTime.Now;

                _context.Blogs.Update(modelOld);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
