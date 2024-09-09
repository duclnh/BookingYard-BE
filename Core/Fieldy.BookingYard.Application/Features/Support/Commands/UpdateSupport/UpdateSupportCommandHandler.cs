using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Commands.UpdateSupport
{
    public class UpdateSupportCommandHandler : IRequestHandler<UpdateSupportCommand, string>
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IJWTService _jWTService;

        public UpdateSupportCommandHandler(ISupportRepository supportRepository, IJWTService jWTService)
        {
            _supportRepository = supportRepository;
            _jWTService = jWTService;
        }

        public async Task<string> Handle(UpdateSupportCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSupportCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid update support", validationResult);

            var support = await _supportRepository.Find(x => x.Id == request.SupportID, cancellationToken);
            if(support == null)
                throw new NotFoundException(nameof(Support), request.SupportID);
            
            support.Note = request.Note;
            support.ModifiedBy = _jWTService.UserID;
            support.ModifiedAt = DateTime.Now;

            _supportRepository.Update(support);
            var result = await _supportRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if(result < 0)
                throw new BadRequestException("Update support fail!");

            return "Update support successfully";
        }
    }
}
