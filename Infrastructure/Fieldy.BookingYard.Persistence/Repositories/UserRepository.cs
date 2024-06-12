using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
        {
        }
    }
}