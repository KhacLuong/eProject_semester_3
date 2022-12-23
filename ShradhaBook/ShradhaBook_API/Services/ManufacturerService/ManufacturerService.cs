using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.ViewModels;




namespace ShradhaBook_API.Services.ManufacturerService
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ManufacturerService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddManufacturerAsync(ManufacturerModel model)
        {
            if (_context.Manufacturers.Any(m => m.Code == model.Code))
            {
                return MyStatusCode.DUPLICATE_CODE;
            }
            if (_context.Manufacturers.Any(c => c.Name == model.Name))
            {
                return MyStatusCode.DUPLICATE_NAME;
            }

            Manufacturer newManufacturer = _mapper.Map<Manufacturer>(model);
            _context.Manufacturers!.Add(newManufacturer);
            await _context.SaveChangesAsync();
            return newManufacturer.Id;
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

        public async Task<ManufacturerModel> GetManufacturerAsync(int id)
        {
            var manufacturer = await _context.Manufacturers!.FindAsync(id);
            return _mapper.Map<ManufacturerModel>(manufacturer);
        }

        public async Task<List<ManufacturerModel>> GetAllManufacturersAsync(string? name, string? code)
        {
            var allManufacturers = await _context.Manufacturers
           .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
           && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
           !.ToListAsync();
           return _mapper.Map<List<ManufacturerModel>>(allManufacturers);
        }

        public async Task<int> UpdateManufacturerAsync(int id, ManufacturerModel model)
        {
            if (id == model.Id)
            {
                if (_context.Manufacturers.Any(m => m.Code == model.Code && m.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_CODE;
                }
                if (_context.Categories.Any(m => m.Name == model.Name && m.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_NAME;
                }
                var updateManufacturer = _mapper.Map<Manufacturer>(model);
                _context.Manufacturers.Update(updateManufacturer);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
