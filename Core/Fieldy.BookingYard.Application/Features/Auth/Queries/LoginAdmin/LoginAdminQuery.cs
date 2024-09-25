using Fieldy.BookingYard.Application.Models.Auth;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Queries.LoginAdmin
{
    public class LoginAdminQuery : IRequest<AuthResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
