using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage
{
	public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IPackageRepository _packageRepository;
        public CreatePackageCommandHandler(IMapper mapper, IPackageRepository packageRepository)
        {
            _mapper = mapper;
            _packageRepository = packageRepository;
        }
        public async Task<string> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePackageCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid register package", validationResult);

            var packageExist = await _packageRepository.Find(x => x.PackageName == request.PackageName, cancellationToken);

			if (packageExist != null)
				throw new ConflictException("Package already exists");

			var package = _mapper.Map<Domain.Entities.Package>(request);
            package.Id = Guid.NewGuid();
            package.CreatedAt = DateTime.Now;
            package.ModifiedAt = DateTime.Now;

            if (package == null)
                throw new BadRequestException("Error create package!");

            package.IsDeleted = false;
			package.CreatedAt = DateTime.Now;
            package.ModifiedAt = DateTime.Now;

            await _packageRepository.AddAsync(package);

            var result = await _packageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result < 0)
                throw new BadRequestException("Create new package fail!");

            return "Create package successfully";
        }
    }
}
