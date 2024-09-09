using AutoMapper;
using Fieldy.BookingYard.Application.Features.Auth.Commands.Google;
using Fieldy.BookingYard.Application.Features.User.Queries;
using Fieldy.BookingYard.Application.Models.User;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<GoogleCommand, User>();
            CreateMap<User, UserUpdateDTO>();
        }
    }
}