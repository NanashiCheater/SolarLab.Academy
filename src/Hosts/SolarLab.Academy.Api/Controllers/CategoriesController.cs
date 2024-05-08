using Microsoft.AspNetCore.Mvc;
using System.Net;
using SolarLab.Academy.AppServices.Categories.Services;
using SolarLab.Academy.Domain.Categories.Entity;
using SolarLab.Academy.Contracts.Categories;
using Microsoft.AspNetCore.Authorization;
using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Universal;
using Microsoft.Build.Execution;

namespace SolarLab.Academy.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями <see cref="Category"/>.
    /// </summary>
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список категорий.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция получения всех категорий.");
            _logger.LogInformation("Запрос на получение всех категорий.");
            var result = await _categoryService.GetAll(cancellationToken);
            _logger.LogInformation("Запрос на получение всех категорий выполнен успешно.");
            return result;
            
        }

        /// <summary>
        /// Получить информацию о категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о категории.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция получения категории по идентификатору {Id}.",id);
            _logger.LogInformation("Запрос получения категории по идентификатору {Id}.", id);
            var result = await _categoryService.Get(id, cancellationToken);

            if (result == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            _logger.LogInformation("Запрос получения категории по идентификатору {Id} выполнен успешно.", id);
            return Ok(result);
        }

        /// <summary>
        /// Добавить категорию.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<Guid> AddCategory(CreateCategoryRequest model, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция создания категории по запросу {Request}.", model);
            _logger.LogInformation("Запрос на создание категории с названием {Name}.",model.Name);
            var result = await _categoryService.AddAsync(model, cancellationToken);
            _logger.LogInformation("Запрос на создание категории с названием {Name} выполнен успешно.", model.Name);
            return result;
        }
        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция удаления категории по идентификатору {Id}.", id);
            _logger.LogInformation("Запрос удаления категории по идентификатору {Id}.", id);
            await _categoryService.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Запрос удаления категории по идентификатору {Id} успешно.", id);
            return Ok();
        }
        
        /// <summary>
        /// Обновить данные комментария.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        public async Task<CategoryDto> UpdateCategory(CategoryDto model, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция обновления категории по запросу {Request}.", model);
            _logger.LogInformation("Запрос обновления категории по идентификатору {Id}.", model.Id);
            var result = await _categoryService.UpdateAsync(model, cancellationToken);
            _logger.LogInformation("Запрос обновления категории по идентификатору {Id} выполнен успешно.", model.Id);
            return result;
        }
    }
}
