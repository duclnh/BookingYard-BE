using System;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class FacilityRepository : RepositoryBase<Facility, Guid>, IFacilityRepository
{
    public FacilityRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
    {
    }
}
