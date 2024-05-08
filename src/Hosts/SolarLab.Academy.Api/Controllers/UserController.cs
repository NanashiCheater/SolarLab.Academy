using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.Api.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route("[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class UserController(IUserService userService, IConfiguration configuration, UserManager<Domain.Users.Entity.User> userManager, IUserRoleService userRoleService, ILogger<UserController> logger) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<Domain.Users.Entity.User> _userManager = userManager;
    private readonly IUserRoleService _userRoleService = userRoleService;
    public readonly ILogger<UserController> _logger = logger;
    /// <summary>
    /// Возвращает список пользователей. 
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    [Authorize]
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(ResultWithPagination<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllRequest request, CancellationToken cancellationToken)
    {
        using var loggerscope = _logger.BeginScope("Операция получения пользователей с пагинацией с номером страницы {PageNumber} и размером выборки {BatchSize}.", request.PageNumber, request.Batchsize);
        _logger.LogInformation("Запрос на получение пользователей с пагинациейс номером страницы {PageNumber} и размером выборки {BatchSize}.", request.PageNumber, request.Batchsize);
        var result = await _userService.GetUsersAsync(request, cancellationToken);
        _logger.LogInformation("Запрос на получение пользователей с пагинациейс номером страницы {PageNumber} и размером выборки {BatchSize} выполнен успешно.", request.PageNumber, request.Batchsize);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает пользователя по id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пользователь.</returns>
    [Authorize]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        using var loggerscope = _logger.BeginScope("Операция получения пользователя по идентификатору {Id}.", id);
        _logger.LogInformation("Запрос на получение пользователя по идентификатору {Id}.", id);
        var result = await _userService.GetByIdAsync(id, cancellationToken);
        _logger.LogInformation("Запрос на получение пользователя по идентификатору {Id} выполнен успешно.", id);
        return Ok(result);
    }
    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновлённые данные.</returns>
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPut]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        using var loggerscope = _logger.BeginScope("Операция обновления данных пользователя по запросу {Request}.", request);
        _logger.LogInformation("Запрос на обновление пданных пользователя.");
        var result = await _userService.UpdateAsync(request, cancellationToken);
        _logger.LogInformation("Запрос на обновление пданных пользователя выполнен успешно.");
        return Ok(result);
    }
    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        using var loggerscope = _logger.BeginScope("Операция удаления пользователя по идентификатору {Id}.", id);
        _logger.LogInformation("Запрос на удаление пользователяпо идентификатору {Id}.", id);
        await _userService.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Запрос на удаление пользователяпо идентификатору {Id} выполнен успешно.", id);
        return Ok();
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="createUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        using var loggerscope = _logger.BeginScope("Операция создания пользователя по запросу {Request}.", createUserRequest);
        _logger.LogInformation("Запрос на создание пользователя.");
        var result = await _userService.AddAsync(createUserRequest, cancellationToken);
        _logger.LogInformation("Запрос на создание пользователя выполнен успешно.");
        return Ok(result);
    }


    /// <summary>
    /// Аутентификация пользоваителя.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("Authenticate")]
    public async Task<IActionResult> Authenticate([FromQuery]AuthenticateRequest request, CancellationToken cancellationToken)
    {
        using var loggerscope = _logger.BeginScope("Операция аутентификации пользователя с почтой {Email}", request.Email);
        _logger.LogInformation("Запрос на аутентификацию пользователя с почтой {Email}",request.Email);
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return BadRequest("Bad credentials");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }
        var role = await _userRoleService.GetUserRoleById(user.Id, cancellationToken);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, role.Name)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            null,
            null,
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);
        _logger.LogInformation("Запрос на аутентификацию пользователя с почтой {Email}", request.Email);
        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}