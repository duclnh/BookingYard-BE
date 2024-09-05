using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, string>
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IMapper _mapper;
        public CreateContactCommandHandler(ISupportRepository supportRepository,
                                          IMapper mapper)
        {
            _supportRepository = supportRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateContactCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid register contact", validationResult);

            var contactExist = await _supportRepository.Find(x => x.Email == request.Email
                                                          && x.Phone == request.Phone
                                                          && x.TypeSupport == TypeSupport.Contact, cancellationToken);

            if (contactExist != null && !contactExist.IsProcessed())
                throw new ConflictException("Contact already exists and has not been processed!");

            var contact = _mapper.Map<Domain.Entities.Support>(request);

            if (contact == null)
                throw new BadRequestException("Error create contact!");

            contact.TypeSupport = TypeSupport.Partner;
            contact.CreatedAt = DateTime.UtcNow;

            await _supportRepository.AddAsync(contact);

            var result = await _supportRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result < 0)
                throw new BadRequestException("Create new contact fail!");


            return "Create contact successfully";
        }
    }
}
