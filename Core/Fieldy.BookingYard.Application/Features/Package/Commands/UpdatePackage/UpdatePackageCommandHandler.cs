using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.UpdatePackage
{
	internal class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IPackageRepository _packageRepository;
		public UpdatePackageCommandHandler(IMapper mapper, IPackageRepository packageRepository)
		{
			_mapper = mapper;
			_packageRepository = packageRepository;
		}
		public async Task<string> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdatePackageCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid update package", validationResult);

			var package = await _packageRepository.Find(x => x.PackageName == request.PackageName && x.Id != request.PackageId, cancellationToken);
			if (package != null)
				throw new ConflictException("Package already exists");

			package = await _packageRepository.Find(x => x.Id == request.PackageId, cancellationToken);
			if (package == null)
				throw new NotFoundException(nameof(package), request.PackageId);

			/*package = _mapper.Map<Domain.Entities.Package>(request);*/
			if (!string.IsNullOrEmpty(request.PackageName))
			{
				package.PackageName = request.PackageName;
			}
			if(!string.IsNullOrEmpty(request.PackageDescription))
				package.PackageDescription = request.PackageDescription;
			if(request.PackagePrice != 0)
				package.PackagePrice = request.PackagePrice;
			package.ModifiedAt = DateTime.Now;
			_packageRepository.Update(package);

			var result = await _packageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			if (result < 0)
				throw new BadRequestException("Update package fail!");

			return "Update package successfully";
		}
	}
}
