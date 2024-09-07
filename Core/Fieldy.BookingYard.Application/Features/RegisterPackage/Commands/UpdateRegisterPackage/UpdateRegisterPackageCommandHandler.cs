﻿using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.UpdateRegisterPackage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.UpdateRegisterPackage
{
	public class UpdateRegisterPackageCommandHandler : IRequestHandler<UpdateRegisterPackageCommand, string>
	{

		private readonly IMapper _mapper;
		private readonly IRegisterPackageRepository _registerPackageRepository;
		public UpdateRegisterPackageCommandHandler(IMapper mapper, IRegisterPackageRepository registerPackageRepository)
		{
			_mapper = mapper;
			_registerPackageRepository = registerPackageRepository;
		}
		public async Task<string> Handle(UpdateRegisterPackageCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateRegisterPackageCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register package", validationResult);

			var registerPackage = _mapper.Map<Domain.Entities.RegisterPackage>(request);

			if (registerPackage == null)
				throw new BadRequestException("Error Update register package!");

			var registerPackageExist = await _registerPackageRepository.Find(x => x.RegisterPackageID == request.RegisterPackageID, cancellationToken);
			if (registerPackageExist == null)
				throw new BadRequestException("Register package not found!");

			registerPackage = _mapper.Map<Domain.Entities.RegisterPackage>(request);
			registerPackage.ModifiedAt = DateTime.Now;

			await _registerPackageRepository.AddAsync(registerPackage);

			var result = await _registerPackageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update register package fail!");

			return "Update register package successfully";
		}
	}
}
