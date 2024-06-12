using AutoMapper;
using Fieldy.BookingYard.Application.Features.Auth.Commands.Google;
using Fieldy.BookingYard.Application.Features.Users.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserDTO>().ForMember(dest => dest.UserID, 
                                                option => option.MapFrom(src => src.Id));
            CreateMap<GoogleCommand, User>();
        }
    }
}