using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Commands.CreateAdvise
{
    public class CreateAdviseCommandHandler : IRequestHandler<CreateAdviseCommand, string>
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IMapper _mapper;
        public CreateAdviseCommandHandler(ISupportRepository supportRepository,
                                          IMapper mapper)
        {
            _supportRepository = supportRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateAdviseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAdviseCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid register advise", validationResult);

            var adviseExist = await _supportRepository.Find(x => x.Email == request.Email
                                                           && x.Phone == request.Phone
                                                           && x.TypeSupport == TypeSupport.Partner, cancellationToken);

            if (adviseExist != null && !adviseExist.IsProcessed())
                throw new ConflictException("Advice already exists and has not been processed!");

            var advise = _mapper.Map<Domain.Entities.Support>(request);

            if (advise == null)
                throw new BadRequestException("Error create advise!");

            advise.TypeSupport = TypeSupport.Partner;
            advise.CreatedAt = DateTime.Now;

            await _supportRepository.AddAsync(advise);

            var result = await _supportRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result < 0)
                throw new BadRequestException("Create new advise fail!");

            return "Create advise successfully";
        }
    }
}
