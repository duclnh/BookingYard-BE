using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Discount.Command.CreateDiscount
{
	public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IDiscountRepository _DiscountRepository;
		public CreateDiscountCommandHandler(IMapper mapper, IDiscountRepository DiscountRepository)
		{
			_mapper = mapper;
			_DiscountRepository = DiscountRepository;
		}
		public async Task<string> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateDiscountCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Discount", validationResult);

			var DiscountExist = await _DiscountRepository.Find(x => x.DiscountName == request.DiscountName, cancellationToken);

			if (DiscountExist != null)
				throw new ConflictException("Discount already exists");

			var Discount = _mapper.Map<Domain.Entities.Discount>(request);

			if (Discount == null)
				throw new BadRequestException("Error create Discount!");

			await _DiscountRepository.AddAsync(Discount);

			var result = await _DiscountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new Discount fail!");

			return "Create Discount successfully";
		}
	}
}
