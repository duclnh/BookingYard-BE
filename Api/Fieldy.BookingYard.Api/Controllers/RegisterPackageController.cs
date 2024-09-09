using Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.CreateRegisterPackage;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.UpdateRegisterPackage;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Queries;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Queries.GetAllRegisterPackage;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/registerpackage")]
	[ApiController]
	public class RegisterPackageController : ControllerBase
	{
		private readonly IMediator _mediator;
		public RegisterPackageController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateRegisterPackage(
						[FromBody] CreateRegisterPackageCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Created(string.Empty, result);
		}

		[HttpPut]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateRegisterPackage(
						[FromBody] UpdateRegisterPackageCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[HttpPost()]
		[Route("Paging")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(RegisterPackageDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllRegisterPackage(
						[FromBody] RequestParams requestParams,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllRegisterPackageQuery(requestParams), cancellationToken);
			return Ok(result);
		}
	}
}
