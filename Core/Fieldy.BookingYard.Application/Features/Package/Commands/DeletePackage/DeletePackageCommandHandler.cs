using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.DeletePackage
{
	internal class DeletePackageCommandHandler : IRequestHandler<DeletePackageCommand, string>
	{
		private readonly IPackageRepository _packageRepository;
		private readonly IRegisterPackageRepository _registerPackageRepository;
		public DeletePackageCommandHandler(IPackageRepository packageRepository, IRegisterPackageRepository registerPackageRepository)
		{
			_packageRepository = packageRepository;
			_registerPackageRepository = registerPackageRepository;
		}
		public async Task<string> Handle(DeletePackageCommand request, CancellationToken cancellationToken)
		{
			var package = await _packageRepository.Find(x => x.Id == request.PackageId, cancellationToken);
			if (package == null)
				throw new NotFoundException(nameof(package), request.PackageId);

			IList<Domain.Entities.RegisterPackage> registerPackages = await _registerPackageRepository.FindAll(
										expression: x => x.PackageID == request.PackageId,
										cancellationToken: cancellationToken);

			if(registerPackages != null)
			{
				package.IsDeleted = true;
				_packageRepository.Update(package);
			}
			else
			{
				_packageRepository.Remove(package);
			}

			var result = await _packageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			if (result < 0)
				throw new BadRequestException("Delete package fail!");

			return "Delete package successfully";
		}
	}
}
