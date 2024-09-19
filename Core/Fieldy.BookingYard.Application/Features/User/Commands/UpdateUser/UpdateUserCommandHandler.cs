using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJWTService _jwtService;
    private readonly IUtilityService _utilityService;

    public UpdateUserCommandHandler(IUserRepository userRepository, IJWTService jwtService, IUtilityService utilityService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _utilityService = utilityService;
    }

    public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        List<string> oldImage = new List<string>();
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid update user", validationResult);

        var user = await _userRepository.Find(x => x.Id == request.UserId);

        if (user == null)
            throw new NotFoundException(nameof(user), request.UserId);

        var userUpdate = await _userRepository.Find(x => x.Id == _jwtService.UserID);

        if (userUpdate != null && userUpdate.Id != user.Id)
            throw new BadRequestException($"You don't update user have ID: {request.UserId}");

        if(user.ImageUrl != null){
            oldImage.Add(user.ImageUrl);
        }

        user.Name = request.Name ?? user.Name;
        user.Address = request.Address ?? user.Address;
        user.Phone = request.Phone ?? user.Phone;
        user.Gender = request.Gender != "other" ? request.Gender == "female" ? Gender.Female : Gender.Male : Gender.Other;
        user.WardID = request.WardID ?? user.WardID;
        user.ImageUrl = request.Image != null ? await _utilityService.AddFile(request.Image, $"User/{user.Id}") : user.ImageUrl;

        _userRepository.Update(user);
        var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (result < 0)
            throw new BadRequestException("Update user fail!");

        _utilityService.RemoveFile(oldImage);

        return "Update user successfully";
    }
}
