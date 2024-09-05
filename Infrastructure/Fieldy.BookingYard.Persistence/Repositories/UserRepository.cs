using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, User>, IUserRepository
    {
        public UserRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
        {

        }
    }
}