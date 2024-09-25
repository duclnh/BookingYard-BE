using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
        {

        }
    }
}