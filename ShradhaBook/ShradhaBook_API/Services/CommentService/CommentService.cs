using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;



        public CommentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async  Task<int> AddCommentAsync(CommentModelPost model)
        {
            if (model.Content == null || model.Content.Trim().Length == 0)
            {
                return MyStatusCode.FAILURE;
            }
            var checkRateExists = await _context.Rates.SingleAsync(r => r.UserId == model.UserId && r.ProductId == model.ProductId);
            if (checkRateExists==null)
            {
                return MyStatusCode.NOTFOUND_RATE;
            }
            var checkCommentExists = await _context.Comments.AnyAsync(r => r.UserId == model.UserId && r.ProductId == model.ProductId);
            if (!checkCommentExists)
            {
                return MyStatusCode.FAILURE;
            }
            var newModel = _mapper.Map<Comment>(model);
            newModel.RateId= checkRateExists.Id;
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = null;
            _context.Comments!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;

        }

        public async Task DeleteCommentAsync(int id)
        {
            var model = await _context.Comments!.FindAsync(id);
            if (model != null)
            {
                _context.Comments!.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public  async Task<CommentModelGet> GetCommentById(int id)
        {
            var model = await _context.Comments!.FindAsync(id);
           return _mapper.Map<CommentModelGet>(model);
        }

        public async  Task<int> UpdateCommentAsync(int id, CommentModelPost model)
        {
            if (id == model.Id)
            {
                var modelOld = await _context.Comments.FindAsync(id);

                if (modelOld == null)
                {
                    return MyStatusCode.FAILURE;
                }
                if (model.Content == null || model.Content.Trim().Length == 0)
                {
                    return MyStatusCode.FAILURE;
                }
                var checkRateExists = await _context.Rates.SingleAsync(r => r.UserId == model.UserId && r.ProductId == model.ProductId);
                if (checkRateExists == null)
                {
                    return MyStatusCode.FAILURE;
                }
                var checkCommentExists = await _context.Comments.AnyAsync(r => r.UserId == model.UserId && r.ProductId == model.ProductId && r.Id!=model.Id);
                if (!checkCommentExists)
                {
                    return MyStatusCode.FAILURE;
                }

                var newModel = _mapper.Map<Comment>(model);
                newModel.CreatedAt = modelOld.CreatedAt;
                newModel.UpdatedAt = DateTime.Now;
                _context.Comments.Update(newModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
