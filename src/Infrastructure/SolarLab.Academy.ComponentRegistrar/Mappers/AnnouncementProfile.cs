using AutoMapper;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль работы с объявлениями.
    /// </summary>
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>();

            CreateMap<CreateAnnouncementRequest, Announcement>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.Owner, map => map.Ignore())
                .ForMember(s => s.Category, map => map.Ignore());

            CreateMap<UpdateInfoRequest, Announcement>()
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.Owner, map => map.Ignore())
                .ForMember(s => s.Category, map => map.Ignore());
        }
    }
}
