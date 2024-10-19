using System;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Commands.BanUser;

public class BanUserCommandHandler : IRequestHandler<BanUserCommand, string>
{
    private readonly IUserRepository _userRepository;

    public BanUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.UserID, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(user), request.UserID);

        user.IsDeleted = !user.IsDeleted;
        _userRepository.Update(user);

        var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (result < 0)
            throw new BadRequestException("Update fail");

        return "Update successfully";
    }
}
