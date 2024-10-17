using AutoMapper;
using Fieldy.BookingYard.Application.Features.Auth.Commands.Google;
using Fieldy.BookingYard.Application.Features.User.Queries;
using Fieldy.BookingYard.Application.Features.User.Queries.DTO;
using Fieldy.BookingYard.Application.Models.User;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, ManagerDTO>();
            CreateMap<User, UserAdminDTO>()
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MM-yyyy hh\\:mm")));

            CreateMap<GoogleCommand, User>();
            CreateMap<User, UserUpdateDTO>();
        }
    }
}