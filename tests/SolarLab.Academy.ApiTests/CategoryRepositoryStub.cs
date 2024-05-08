using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Domain.Categories.Entity;
using System.Linq.Expressions;

namespace SolarLab.Academy.ApiTests
{
    public class CategoryRepositoryStub : ICategoryRepository
    {
        public static CategoryDto[] AllCategories { get; } =
            new[]
            {
                new CategoryDto { Id = Guid.NewGuid(), Name = "test1" },
                new CategoryDto { Id = Guid.NewGuid(), Name = "test2" }
            };

        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return AllCategories;
        }

        public Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetFiltered(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Category?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Category model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
