using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarLab.Academy.AppServices.Comments.Services;
using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Domain.Comments.Entity;

namespace SolarLab.Academy.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с комментариями.
    /// </summary>
    /// <param name="commentService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(ICommentService commentService, ILogger<CommentController> logger) : ControllerBase
    {
        private readonly ICommentService _commentService = commentService;
        private readonly ILogger<CommentController> _logger = logger;
        /// <summary>
        /// Добавить комментарий.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<Guid> AddComment(CreateCommentRequest model, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция создания комментария по запросу {Request}.", model);
            _logger.LogInformation("Запрос на создание комментария к объявлению с идентификатором {Id}.",model.AnnouncementId);
            var result = await _commentService.AddAsync(model, cancellationToken);
            _logger.LogInformation("Запрос на создание комментария к объявлению с идентификатором {Id} выполнен успешно.", model.AnnouncementId);
            return result;
        }
        /// <summary>
        /// Удалить все комментарии объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("DeleteAll")]
        public async Task DeleteAllCommentsByAnnouncementId(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция удаления всех комментариев объявления с идентификатором {Id}.", id);
            _logger.LogInformation("Запрос удаления всех комментариев объявления с идентификатором {Id}.", id);
            await _commentService.DeleteAllByAnnouncementIdAsync(id, cancellationToken);
            _logger.LogInformation("Запрос удаления всех комментариев объявления с идентификатором {Id} выполнен успешно.", id);
        }
        /// <summary>
        /// Удалить комментарий.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task DeleteComment(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция удаления комментария с идентификатором {Id}.", id);
            _logger.LogInformation("Запрос удаления комментария с идентификатором {Id}.", id);
            await _commentService.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Запрос удаления комментария с идентификатором {Id} выполнен успешно.", id);
        }
        /// <summary>
        /// Получить коментарии с пагинацией.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ResultWithPagination<CommentDto>> GetComments([FromQuery] GetAllCommentsRequest request, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция получения комментариев с пагинацией с номером страницы {PageNumber}, размером выборки {BatchSize} и идентификатором объявления {Id}.", request.PageNumber, request.Batchsize, request.AnnouncementId);
            _logger.LogInformation("Запрос получения комментариев с пагинацией с номером страницы {PageNumber}, размером выборки {BatchSize} и идентификатором объявления {Id}.", request.PageNumber, request.Batchsize, request.AnnouncementId);
            var result = await _commentService.GetCommentsAsync(request, cancellationToken);
            _logger.LogInformation("Запрос получения комментариев с пагинацией с номером страницы {PageNumber}, размером выборки {BatchSize} и идентификатором объявления {Id} выполнен успешно.", request.PageNumber, request.Batchsize, request.AnnouncementId);
            return result;
        }
        /// <summary>
        /// Обновить данные комментария.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut]
        public async Task<CommentDto> UpdateComment(UpdateCommentRequest model, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция обновления комментария по запросу {Request}.", model);
            _logger.LogInformation("Запрос на обновление комментария с идентификатором {Id}.", model.Id);
            var result = await _commentService.UpdateAsync(model, cancellationToken);
            _logger.LogInformation("Запрос на обновление комментария с идентификатором {Id} выполнен успешно.", model.Id);
            return result;
        }
    }
}
