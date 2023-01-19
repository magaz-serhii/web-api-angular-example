using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Products.WebApi.DTO.Manufacturers;
using Products.WebApi.Models.Manufacturers;
using Products.WebApi.Exceptions;

namespace Products.WebApi.Services
{
    public class ManufacturersService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IValidator<CreateManufacturerMadel> _createManufacturerValidator;
        private readonly IValidator<UpdateManufacturerMadel> _updateManufacturerValidator;

        public ManufacturersService(
            IMapper mapper,
            AppDbContext dbContext,
            IValidator<CreateManufacturerMadel> createManufacturerValidator,
            IValidator<UpdateManufacturerMadel> updateManufacturerValidator)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _createManufacturerValidator = createManufacturerValidator;
            _updateManufacturerValidator = updateManufacturerValidator;
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
            if (manufacturerDto == null)
            {
                throw new EntityNotFoundException($"Manufacturer {id} not found");
            }

            return manufacturerDto;
        }

        public async Task<ManufacturerDto> CreateManufacturerAsync(CreateManufacturerMadel model)
        {
            await this._createManufacturerValidator.ValidateAndThrowAsync(model);
            
            var manufacturer = this._mapper.Map<Manufacturer>(model);
            await _dbContext.Manufacturers.AddAsync(manufacturer);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ManufacturerDto>(manufacturer);
        }

        public async Task UpdateManufacturerAsync(long id, UpdateManufacturerMadel model)
        {
            await this._updateManufacturerValidator.ValidateAndThrowAsync(model);

            var manufacturer = await this._dbContext.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                throw new EntityNotFoundException($"Manufacturer {id} not found");
            }

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


