using AutoMapper;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Specifications;
using SolarLab.Academy.Contracts.Universal;

namespace SolarLab.Academy.AppServices.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public Task<ResultWithPagination<UserDto>> GetUsersAsync(GetAllRequest request, CancellationToken cancellationToken)
    {
        return _userRepository.GetAll(request, cancellationToken);
    }
    /// <inheritdoc />
    public Task<IEnumerable<UserDto>> GetUsersByNameAsync(UsersByNameRequest request, CancellationToken cancellationToken)
    {
        Specification<User> specification = new ByNameSpecification(request.Name);

        if (request.IsOlder18)
        {
            specification = specification.And(new Older18specification());
        }
        
        return _userRepository.GetAllBySpecification(specification, cancellationToken);
    }
    /// <inheritdoc />
    public async ValueTask<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<Guid> AddAsync(CreateUserRequest model, CancellationToken cancellationToken)
    {
        var result = await _userRepository.AddAsync(model, cancellationToken);
        return result;
    }
    /// <inheritdoc />
    public async Task<UpdateUserRequest> UpdateAsync(UpdateUserRequest model, CancellationToken cancellationToken)
    {       
        return await _userRepository.UpdateAsync(model, cancellationToken);
    }
    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
    }
}