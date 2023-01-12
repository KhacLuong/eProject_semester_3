using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.ManufacturerService;

public class ManufacturerService : IManufacturerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ManufacturerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddManufacturerAsync(ManufacturerModelPost model)
    {
        if (model.PhoneNumber.Trim() != null && !Helpers.Helpers.IsValidPhone(model.PhoneNumber))
            return MyStatusCode.PHONE_INVALID;
        if (model.Email.Trim() != null && !Helpers.Helpers.IsValidEmail(model.Email)) return MyStatusCode.EMAIL_INVALID;

        if (model.Code.Trim().Length < 3 || !Helpers.Helpers.IsValidCode(model.Code)) return MyStatusCode.FAILURE;
        if (model.Name.Trim() == null || model.Name.Trim().Length < 1) return MyStatusCode.FAILURE;
        if (_context.Manufacturers.Any(m => m.Code.Trim() == model.Code.Trim())) return MyStatusCode.DUPLICATE_CODE;
        if (model.Email.Trim() != null && _context.Manufacturers.Any(m => m.Email.Trim() == model.Code.Trim()))
            return MyStatusCode.DUPLICATE_EMAIL;
        if (model.PhoneNumber.Trim() != null &&
            _context.Manufacturers.Any(m => m.PhoneNumber.Trim() == model.PhoneNumber.Trim()))
            return MyStatusCode.DUPLICATE_PHONE;

        var newModel = _mapper.Map<Manufacturer>(model);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.Manufacturers!.Add(newModel);
        await _context.SaveChangesAsync();
        return newModel.Id;
    }

    public async Task DeleteManufacturerAsync(int id)
    {
        var manufacturer = _context.Manufacturers!.SingleOrDefault(c => c.Id == id);
        if (manufacturer != null)
        {
            _context.Manufacturers!.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ManufacturerModelGet> GetManufacturerAsync(int id)
    {
        var manufacturer = await _context.Manufacturers!.FindAsync(id);
        return _mapper.Map<ManufacturerModelGet>(manufacturer);
    }

    public async Task<object> GetAllManufacturersAsync(string? name, string? code, int pageSize = 20, int pageIndex = 1)
    {
        var allModel = await _context.Manufacturers
            .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
                        && m.Code.ToUpper().Contains(string.IsNullOrEmpty(code) ? "" : code.ToUpper().Trim()))
            !.ToListAsync();
        var models = PaginatedList<Manufacturer>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<Manufacturer>.totlalPage(allModel, pageSize);
        var result = _mapper.Map<List<ManufacturerModelGet>>(models);
        return new
        {
            Manufacturers = result, totalPage
        };
    }

    public async Task<int> UpdateManufacturerAsync(int id, ManufacturerModelPost model)
    {
        if (id == model.Id)
        {
            if (model.PhoneNumber.Trim() != null && !Helpers.Helpers.IsValidPhone(model.PhoneNumber))
                return MyStatusCode.PHONE_INVALID;
            if (model.Email.Trim() != null && !Helpers.Helpers.IsValidEmail(model.Email))
                return MyStatusCode.EMAIL_INVALID;
            if (model.Code.Trim().Length < 3 || !Helpers.Helpers.IsValidCode(model.Code)) return MyStatusCode.FAILURE;
            if (model.Name.Trim() == null || model.Name.Trim().Length < 1) return MyStatusCode.FAILURE;
            if (_context.Manufacturers.Any(m => m.Code == model.Code && m.Id != model.Id))
                return MyStatusCode.DUPLICATE_CODE;
            if (model.Email.Trim() != null &&
                _context.Manufacturers.Any(m => m.Email.Trim() == model.Email.Trim() && m.Id != model.Id))
                return MyStatusCode.DUPLICATE_EMAIL;
            if (model.PhoneNumber.Trim() != null && _context.Manufacturers.Any(m =>
                    m.PhoneNumber.Trim() == model.PhoneNumber.Trim() && m.Id != model.Id))
                return MyStatusCode.DUPLICATE_PHONE;
            var modelOld = await _context.Manufacturers.FindAsync(id);
            modelOld.Name = model.Name;
            modelOld.Code = model.Code;
            modelOld.Email = model.Email;
            modelOld.PhoneNumber = model.PhoneNumber;
            modelOld.Address = model.Address;
            modelOld.UpdatedAt = DateTime.Now;
            _context.Manufacturers.Update(modelOld);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }

    public async Task<List<ManufacturerModelGet>> GetAllManufacturersAsync()
    {
        var allModel = await _context.Manufacturers.ToListAsync();
        return _mapper.Map<List<ManufacturerModelGet>>(allModel);
    }
}