using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.CreateRegisterPackage
{
	public class CreateRegisterPackageCommandHandler : IRequestHandler<CreateRegisterPackageCommand, string>
	{

		private readonly IMapper _mapper;
		private readonly IRegisterPackageRepository _registerPackageRepository;
		public CreateRegisterPackageCommandHandler(IMapper mapper, IRegisterPackageRepository registerPackageRepository)
		{
			_mapper = mapper;
			_registerPackageRepository = registerPackageRepository;
		}
		public async Task<string> Handle(CreateRegisterPackageCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateRegisterPackageCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register package", validationResult);

			var registerPackage = _mapper.Map<Domain.Entities.RegisterPackage>(request);

			if (registerPackage == null)
				throw new BadRequestException("Error create register package!");

			registerPackage = _mapper.Map<Domain.Entities.RegisterPackage>(request);
			registerPackage.CreatedAt = DateTime.Now;
			registerPackage.ModifiedAt = DateTime.Now;

			await _registerPackageRepository.AddAsync(registerPackage);

			var result = await _registerPackageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new register package fail!");

			return "Create register package successfully";
		}
	}
}
