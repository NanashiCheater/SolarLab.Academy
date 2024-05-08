using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Anouncements.Services;
using SolarLab.Academy.AppServices.Comments.Services;
using SolarLab.Academy.AppServices.Files.Services;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Files;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Announcements.Entity;
using System.Drawing;
using System.IO.Compression;
using System.Net;

namespace SolarLab.Academy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class AnnouncementController(IAnnouncementService announcementService, IFileService fileService, IAnnouncementImageService announcementImageService, ICommentService commentService, ILogger<AnnouncementController> logger) : ControllerBase
    {
        private readonly IAnnouncementService _announcementService = announcementService;
        private readonly IFileService _fileService = fileService;
        private readonly IAnnouncementImageService _announcementImageService = announcementImageService;
        private readonly ICommentService _commentService = commentService;
        private readonly ILogger<AnnouncementController> _logger = logger;
        /// <summary>
        /// Получение информации о объявлениях с пагинацией.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(ResultWithPagination<AnnouncementDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllAnnouncements([FromQuery] GetAllRequest request, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция получения объявлений с пагинацией с номером страницы {PageNumber} и размером выборки {BatchSize}.", request.PageNumber, request.Batchsize);
            _logger.LogInformation("Запрос на получение объявлений с пагинацией.");
            var result = await _announcementService.GetAnnouncementsAsync(request, cancellationToken);
            _logger.LogInformation("Запрос на получение объявлений с пагинацией выполнен успешно.");
            return Ok(result);
        }

        /// <summary>
        /// Поиск оюъявления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(ResultWithPagination<AnnouncementDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SearchAnnouncements([FromQuery] SearchAnnouncementRequest request, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция поиска объявлений по слову {Word}.", request.SearchWord);
            _logger.LogInformation("Запрос на поиск оюъявлений по слову {Word}",request.SearchWord);
            var result = await _announcementService.SearchAsync(request, cancellationToken);
            _logger.LogInformation("Запрос на поиск оюъявлений по слову {Word} выполнен успешно.", request.SearchWord);
            return Ok(result);
        }


        /// <summary>
        /// Создать объявление.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор объявления.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAnnouncement(CreateAnnouncementRequest request, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция создания объявления по запросу {Request}.", request);
            _logger.LogInformation("Запрос на создание объявления");
            var announcementId = await _announcementService.AddAsync(request, cancellationToken);
            _logger.LogInformation("Информация объявления добавлена успешно.");
            var images = request.Images;
            foreach (var image in images)
            {
                var bytes = await GetBytesAsync(image, cancellationToken);

                var fileDto = new FileDto
                {
                    Name = image.FileName,
                    Content = bytes,
                    ContentType = image.ContentType,
                };

                var imageiId = await _fileService.UploadAsync(fileDto, cancellationToken);

                var anniuncementImageBound = new AnnouncementImageDto
                {
                    AnnouncementId = announcementId,
                    ImageId = imageiId
                };

                await _announcementImageService.AddAsync(anniuncementImageBound, cancellationToken);

            }
            _logger.LogInformation("Запрос на создание объявления выполнен успешно");
            return CreatedAtAction(nameof(CreateAnnouncement), new { announcementId });
        }

        /// <summary>
        /// Обновить информацию объявления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateInfo")]
        [ProducesResponseType(typeof(AnnouncementDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAnnouncementInfo(UpdateInfoRequest request, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция обновления информации объявления по запросу {Request}.", request);
            _logger.LogInformation("Запрос на обновление информации объявления.");
            var result = await _announcementService.UpdateInfoAsync(request, cancellationToken);
            _logger.LogInformation("Запрос на обновление информации объявления выполнен успешно.");
            return Ok(result);
        }

        /// <summary>
        /// Обновить изображения объявления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("UpdateImages")]
        [ProducesResponseType(typeof(AnnouncementDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAnnouncementImages(UpdateImagesRequest request, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция обновления фотографий объявления по запросу.");
            _logger.LogInformation("Запрос на обновление фотографий объявления по идентификатору {Id}.",request.Id);
            var imageIds = await _announcementImageService.GetByAnnouncementIdAsync(request.Id, cancellationToken);
            if (imageIds == null)
                return NotFound();
            _logger.LogInformation("Объявление существует.");
            await _announcementImageService.DeleteByAnnouncementId(request.Id, cancellationToken);           
            foreach(var image in imageIds)
            {
                await _fileService.DeleteByIdAsync(image.ImageId, cancellationToken);
            }
            _logger.LogInformation("Старые фотографии объявления удалены");
            var images = request.Images;
            foreach (var image in images)
            {
                var bytes = await GetBytesAsync(image, cancellationToken);

                var fileDto = new FileDto
                {
                    Name = image.FileName,
                    Content = bytes,
                    ContentType = image.ContentType,
                };

                var imageiId = await _fileService.UploadAsync(fileDto, cancellationToken);

                var anniuncementImageBound = new AnnouncementImageDto
                {
                    AnnouncementId = request.Id,
                    ImageId = imageiId
                };

                await _announcementImageService.AddAsync(anniuncementImageBound, cancellationToken);
            }
            _logger.LogInformation("Запрос на обновление фотографий объявления по идентификатору {Id} выполнен успешно.", request.Id);
            return Ok();
        }

        /// <summary>
        /// Получить объявление.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAnnouncementInfo")]
        [ProducesResponseType(typeof(ResultWithPagination<AnnouncementDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAnnouncementById(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция получения объявления по идентифкатору {Id}.",id);
            _logger.LogInformation("Запрос на получение объявления по идентифкатору {Id}.", id);
            var announcement = await _announcementService.GetByIdAsync(id, cancellationToken);
            if (announcement == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            _logger.LogInformation("Запрос на получение объявления по идентифкатору {Id} выполнен успешно.", id);
            return Ok(announcement);
        }

        /// <summary>
        /// Получить изображения объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAnnouncementImages")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAnnouncementImages(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция получения фотографий объявления по идентифкатору {Id}.", id);
            _logger.LogInformation("Запрос на получение фотографий объявления по идентификатору {Id}.", id);
            var images = await _announcementImageService.GetByAnnouncementIdAsync(id, cancellationToken);
            if (images == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            var zipName = $"Images-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var image in images) {
                        var file = await _fileService.DownloadAsync(image.ImageId, cancellationToken);
                        Response.ContentLength += file.Content.Length;
                        var entry = zip.CreateEntry(file.Name);
                        using (var fileStream = new MemoryStream(file.Content))
                        using (var entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                }
                _logger.LogInformation("Запрос на получение фотографий объявления по идентификатору {Id} выполнен успешно.", id);
                return File(ms.ToArray(), "application/zip", zipName);
            }
            
        }
        private static async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
        /// <summary>
        /// Удалить объявление.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAnnouncement(Guid id, CancellationToken cancellationToken)
        {
            using var loggerscope = _logger.BeginScope("Операция удаления объявления по идентифкатору {Id}.", id);
            _logger.LogInformation("Запрос на удаление объявления по идентификатору {Id}.", id);
            await _announcementService.DeleteAsync(id, cancellationToken);
            await _announcementImageService.DeleteByAnnouncementId(id, cancellationToken);
            var images = await _announcementImageService.GetByAnnouncementIdAsync(id, cancellationToken);
            if (images == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            await _commentService.DeleteAllByAnnouncementIdAsync(id, cancellationToken);
            foreach (var image in images)
            {
                await _fileService.DeleteByIdAsync(image.ImageId, cancellationToken);
            }
            _logger.LogInformation("Запрос на удаление объявления по идентификатору {Id} выполнен успешно.", id);
            return Ok();
        }
    }
}
