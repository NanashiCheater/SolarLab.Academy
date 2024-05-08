using AutoMapper;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;
using System.Security.Claims;
using System.Security;

namespace SolarLab.Academy.ComponentRegistrar.Mappers;
/// <summary>
/// Профиль работы с пользователями.
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(s => s.FullName, map => map.MapFrom(s => $"{s.LastName} {s.FirstName} {s.MiddleName}"));

        CreateMap<CreateUserRequest, User>()
            .ForMember(s => s.UserName, map => map.MapFrom(s => s.Login))
            .ForMember(s => s.FirstName, map => map.MapFrom(s => s.Name))
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.Claims, map => map.Ignore())
            .ForMember(s => s.Logins, map => map.Ignore())
            .ForMember(s => s.Tokens, map => map.Ignore())
            .ForMember(s => s.UserRoles, map => map.Ignore())
            .ForMember(s => s.NormalizedUserName, map => map.Ignore())
            .ForMember(s => s.NormalizedUserName, map => map.Ignore())
            .ForMember(s => s.NormalizedEmail, map => map.Ignore())
            .ForMember(s => s.EmailConfirmed, map => map.Ignore())
            .ForMember(s => s.PasswordHash, map => map.Ignore())
            .ForMember(s => s.SecurityStamp, map => map.Ignore())
            .ForMember(s => s.ConcurrencyStamp, map => map.Ignore())
            .ForMember(s => s.PhoneNumberConfirmed, map => map.Ignore())
            .ForMember(s => s.TwoFactorEnabled, map => map.Ignore())
            .ForMember(s => s.LockoutEnd, map => map.Ignore())
            .ForMember(s => s.LockoutEnabled, map => map.Ignore())
            .ForMember(s => s.AccessFailedCount, map => map.Ignore());

    }
}
