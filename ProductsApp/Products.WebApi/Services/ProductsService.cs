using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Products.WebApi.DTO.Products;
using Products.WebApi.Models.Products;

namespace Products.WebApi.Services
{
    public class ProductsService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public ProductsService(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IList<ProductListItemDto>> GetProductsAsync()
        {
            var result = await this._dbContext.Products
                .AsNoTracking()
                .ProjectTo<ProductListItemDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<ProductDto> GetProductByIdAsync(long id)
        {
            var product = await this._dbContext.Products
                .AsNoTracking()
                .ProjectTo<ProductDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductModel model)
        {
            var product = this._mapper.Map<Product>(model);
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(long id, UpdateProductModel model)
        {
            var product = await this._dbContext.Products.FindAsync(id);
            this._mapper.Map(model, product);
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(long id)
        {
            var product = await this._dbContext.Products.FindAsync(id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}


