using Fieldy.BookingYard.Application.Models.Auth;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.Common
{
    public interface ICommonService
    { 
        /// <summary>
        /// This method take UserId from token authentication
        /// </summary>
        /// <returns>An string UserID</returns>
        public string? UserId {get;}
        public string Hash(string content);

        /// <summary>
        /// This method generates a JWT (JSON Web Token) for the given user.
        /// </summary>
        /// <param name="user">The user object for which the JWT is being created. It contains the user's ID, name, role, email, and verification status.</param>
        /// <returns>An AuthResponse object containing the user's information and the generated JWT.</returns>
        public AuthResponse CreateTokenJWT(User user);
        public string GenerationCode();
        public bool Verify(string content, string hash);
    }
}