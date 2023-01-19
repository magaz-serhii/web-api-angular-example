using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Products.WebApi.DTO.Categories;
using Products.WebApi.Models.Categories;
using Products.WebApi.Exceptions;
using FluentValidation;

namespace Products.WebApi.Services
{
    public class CategoriesService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IValidator<CreateCategoryModel> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryModel> _updateCategoryValidator;

        public CategoriesService(
            IMapper mapper,
            AppDbContext dbContext,
            IValidator<CreateCategoryModel> createCategoryValidator,
            IValidator<UpdateCategoryModel> updateCategoryValidator)
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
            this._createCategoryValidator = createCategoryValidator;
            this._updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<IList<CategoryListItemDto>> GetCategoriesAsync()
        {
            var result = await this._dbContext.Categories
                .AsNoTracking()
                .ProjectTo<CategoryListItemDto>(this._mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(long id)
        {
            var categoryDto = await this._dbContext.Categories
                .AsNoTracking()
                .ProjectTo<CategoryDto>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (categoryDto == null)
            {
                throw new EntityNotFoundException($"Category {id} not found.");
            }

            return categoryDto;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryModel model)
        {
            await this._createCategoryValidator.ValidateAndThrowAsync(model);
            var category = this._mapper.Map<Category>(model);
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(long id, UpdateCategoryModel model)
        {
            var category = await this._dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new EntityNotFoundException($"Category {id} not found.");
            }
            await this._updateCategoryValidator.ValidateAndThrowAsync(model);
            this._mapper.Map(model, category);
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(long id)
        {
            var category = await this._dbContext.Categories.FindAsync(id);
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}


