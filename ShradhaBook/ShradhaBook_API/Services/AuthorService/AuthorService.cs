using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.ProductService;
using ShradhaBook_API.ViewModels;
using System.Text.RegularExpressions;

namespace ShradhaBook_API.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public AuthorService(DataContext context, IMapper mapper, IProductService productService)
        {
            this._context = context;
            this._mapper = mapper;
            this._productService = productService;
        }

        public async Task<int> AddAuthorAsync(AuthorModelPost model)
        {

            
            if ( model.Email.Trim()!=null && !Helpers.Helpers.IsValidEmail(model.Email))
            {
                return MyStatusCode.FAILURE;
            }
            if (model.Phone.Trim()!=null&&!Helpers.Helpers.IsValidPhone(model.Phone))
            {
                return MyStatusCode.FAILURE;
            }
            if (model.Name.Trim() == null || model.Name.Trim().Length < 1)
            {
                return MyStatusCode.FAILURE;
            }
            if (model.Email.Trim()!=null&&_context.Authors.Any(c => c.Email.Trim() == model.Email.Trim()))
            {
                return MyStatusCode.DUPLICATE_EMAIL;
            }
            if (model.Phone.Trim() != null&& _context.Authors.Any(c => c.Phone.Trim().Equals(model.Phone.Trim())))
            {
                return MyStatusCode.DUPLICATE_PHONE;
            }
            //model.Slug = Helpers.Helpers.Slugify(model.Name);
            Author newModel = _mapper.Map<Author>(model);
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = DateTime.Now;
            _context.Authors!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }

        public async  Task DeleteAuthorAsync(int id)
        {
            var model = _context.Authors!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.Authors!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Object> GetAllAuthorAsync(string? name,string? phone, int? sortBy = 0, int pageSize = 20, int pageIndex = 1)
        {
          
            var allModel = await _context.Authors
            .Where(m => m.Name.ToLower().Trim().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim()))
            .Where(m => m.Phone.ToLower().Trim().Contains(string.IsNullOrEmpty(phone) ? "" : phone.ToLower().Trim()))
            !.ToListAsync();
           
            var models = PaginatedList<Author>.Create(allModel, pageIndex, pageSize);
            var totalPage = PaginatedList<Author>.totlalPage(allModel, pageSize);
            var result = _mapper.Map<List<AuthorModelGet>>(models);
            return new
            {
                Authors = result,
                totalPage = totalPage
            };


        }

        public async Task<List<AuthorModelGet>> GetAllAuthorAsync()
        {

            var allModel = await _context.Authors.ToListAsync();

            return _mapper.Map<List<AuthorModelGet>>(allModel);
        }

            public async Task<AuthorModelGet> GetAuthorAsync(int id)
        {
            var model = await _context.Authors!.FindAsync(id);
            return _mapper.Map<AuthorModelGet>(model);
        }

        public async Task<int> UpdateAuthorAsync(int id, AuthorModelPost model)
        {
            if (id == model.Id)
            {
                if (model.Phone.Trim() != null && !Helpers.Helpers.IsValidPhone(model.Phone))
                {
                    return MyStatusCode.FAILURE;
                }
                if (model.Email.Trim() != null && !Helpers.Helpers.IsValidEmail(model.Email))
                {
                    return MyStatusCode.FAILURE;
                }

                if (model.Name.Trim() == null || model.Name.Trim().Length < 1)
                {
                    return MyStatusCode.FAILURE;
                }
                if (model.Email.Trim() != null && _context.Authors.Any(m => m.Email.Trim() == model.Email.Trim() && m.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_EMAIL;
                }
                if (model.Phone.Trim() != null && _context.Authors.Any(m => m.Phone.Trim() == model.Phone.Trim() && m.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_PHONE;
                }

                var updateModel = _mapper.Map<Author>(model);
                updateModel.UpdatedAt = DateTime.Now;
                _context.Authors.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
