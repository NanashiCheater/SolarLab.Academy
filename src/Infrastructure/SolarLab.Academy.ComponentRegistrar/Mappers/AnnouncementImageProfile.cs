using AutoMapper;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Announcements.Files;
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
    public class AnnouncementImageProfile : Profile
    {
        public AnnouncementImageProfile()
        {
            CreateMap<AnnouncementImage, AnnouncementImageDto>();

            CreateMap<AnnouncementImageDto, AnnouncementImage>()
                .ForMember(s => s.Announcement, map => map.Ignore())
                .ForMember(s => s.Image, map => map.Ignore());
        }
    }
}
