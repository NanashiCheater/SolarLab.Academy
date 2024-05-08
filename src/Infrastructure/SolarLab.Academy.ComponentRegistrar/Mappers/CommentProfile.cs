using AutoMapper;
using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Comments.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профидь работы с комментариями.
    /// </summary>
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>();

            CreateMap<CommentDto, Comment>()
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.User, map => map.Ignore())
                .ForMember(s => s.Announcement, map => map.Ignore());

            CreateMap<UpdateCommentRequest, Comment>()
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.User, map => map.Ignore())
                .ForMember(s => s.Announcement, map => map.Ignore());

            CreateMap<CreateCommentRequest, Comment>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
                .ForMember(s => s.User, map => map.Ignore())
                .ForMember(s => s.Announcement, map => map.Ignore());
        }
    }
}
