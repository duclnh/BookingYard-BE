﻿using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.Contracts.Persistence
{
	public interface IPackageRepository : IRepositoryBase<Package, Guid>
	{
	}
}