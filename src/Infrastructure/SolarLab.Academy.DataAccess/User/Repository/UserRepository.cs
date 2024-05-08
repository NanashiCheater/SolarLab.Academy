using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess.Base;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : BaseRepository<Domain.Users.Entity.User>, IUserRepository
{
    private readonly IMapper _mapper;

    private readonly UserManager<Domain.Users.Entity.User> _userManager;

    private readonly IUserRoleRepository _userRoleRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserRepository"/>
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="dbContext"></param>
    /// <param name="userManager"></param>
    /// <param name="userRoleRepository"></param>
    public UserRepository(IMapper mapper, DbContext dbContext, UserManager<Domain.Users.Entity.User> userManager, IUserRoleRepository userRoleRepository)
        : base(dbContext)
    {
        _mapper = mapper;
        _userManager = userManager;

        _userRoleRepository = userRoleRepository;
    }

    /// <inheritdoc />
    public async Task<ResultWithPagination<UserDto>> GetAll(GetAllRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<UserDto>();
        
        var query = GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.AvailablePages = elementsCount / request.Batchsize;

        var paginationQuery = await query
            .OrderBy(user => user.Id)
            .Skip(request.Batchsize * (request.PageNumber - 1))
            .Take(request.Batchsize)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }
    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> GetAllBySpecification(Specification<Domain.Users.Entity.User> specification, CancellationToken cancellationToken)
    {
        return await GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
    /// <inheritdoc />
    public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return GetAll().Where(s => s.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
    /// <inheritdoc />
    public async Task<UpdateUserRequest> UpdateAsync(UpdateUserRequest model, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(model.Id);
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.MiddleName = model.MiddleName;
        user.BirthDate = model.BirthDate.Value;
        user.Region = model.Region.Value;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;
        await DbContext.SaveChangesAsync(cancellationToken);
        return model;
    }
    /// <inheritdoc />
    public async Task<Guid> AddAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Users.Entity.User>(request);
        var result = await _userManager.CreateAsync(entity, request.Password);

        if (!result.Succeeded)
            throw new Exception(result.Errors.ToString());

        await _userRoleRepository.AddAsync(new ApplicationUserRole { UserId = entity.Id, RoleId = new Guid("83eb4d48-3354-4ff9-a195-1dc294e366fb") }, cancellationToken);                                                  

        return entity.Id;

    }
}