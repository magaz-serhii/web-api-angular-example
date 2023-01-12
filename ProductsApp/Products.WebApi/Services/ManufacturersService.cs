using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Products.WebApi.DTO.Manufacturers;
using Products.WebApi.Models.Manufacturers;

namespace Products.WebApi.Services
{
    public class ManufacturersService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public ManufacturersService(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<ManufacturerListItemDto>> GetManufacturersAsync()
        {
            var result = await this._dbContext.Manufacturers
                .AsNoTracking()
                .ProjectTo<ManufacturerListItemDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<ManufacturerDto> GetManufacturerByIdAsync(long id)
        {
            var manufacturerDto = await this._dbContext.Manufacturers
                .AsNoTracking()
                .ProjectTo<ManufacturerDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            return manufacturerDto;
        }

        public async Task<ManufacturerDto> CreateManufacturerAsync(CreateManufacturerMadel model)
        {
            var manufacturer = this._mapper.Map<Manufacturer>(model);
            await _dbContext.Manufacturers.AddAsync(manufacturer);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ManufacturerDto>(manufacturer);
        }

        public async Task UpdateManufacturerAsync(long id, UpdateManufacturerMadel model)
        {
            var manufacturer = await this._dbContext.Manufacturers.FindAsync(id);
            this._mapper.Map(model, manufacturer);
            _dbContext.Manufacturers.Update(manufacturer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteManufacturerAsync(long id)
        {
            var manufacturer = await this._dbContext.Manufacturers.FindAsync(id);
            _dbContext.Manufacturers.Remove(manufacturer);
            await _dbContext.SaveChangesAsync();
        }
    }
}


