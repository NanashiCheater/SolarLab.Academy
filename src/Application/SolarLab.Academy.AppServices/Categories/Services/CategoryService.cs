using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Categories.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System.Text.Json;

namespace SolarLab.Academy.AppServices.Categories.Services
{
    /// <inheritdoc cref="ICategoryService"/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        // кеш в памяти приложения
        private readonly IMemoryCache _memoryCache;

        // распределённый кеш
        private readonly IDistributedCache _cache;

        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует экземпляр <see cref="CategoryService"/>
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="memoryCache"></param>
        /// <param name="cache"></param>
        public CategoryService(ICategoryRepository repository, IMemoryCache memoryCache, IDistributedCache cache, IMapper mapper)
        {
            _repository = repository;
            _memoryCache = memoryCache;
            _cache = cache;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var key = "all_categories";

            var categoriesSerialized = await _cache.GetStringAsync(key, cancellationToken);

            IReadOnlyCollection<CategoryDto> categories;
            if (!string.IsNullOrWhiteSpace(categoriesSerialized))
            {
                categories = JsonSerializer.Deserialize<IReadOnlyCollection<CategoryDto>>(categoriesSerialized)!;
                return categories;
            }

            categories = await _repository.GetAll(cancellationToken);

            categoriesSerialized = JsonSerializer.Serialize(categories);
            await _cache.SetStringAsync(key, categoriesSerialized,
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                },
                cancellationToken);

            return categories;
        }

        /// <inheritdoc />
        public async Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var key = $"category_info_{id}";

            if (_memoryCache.TryGetValue(key, out var res))
            {
                return (CategoryDto)res!;
            }

            var dto = await _repository.Get(id, cancellationToken);

            _memoryCache.Set(key, dto, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            return dto;
        }
        /// <inheritdoc />
        public async Task<Guid> AddAsync(CreateCategoryRequest model, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(model, cancellationToken);
            return result;
        }
        /// <inheritdoc />
        public async Task<CategoryDto> UpdateAsync(CategoryDto model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(model);
            var result = await _repository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<CategoryDto>(result);
        }
        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}