using Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage;
using Fieldy.BookingYard.Application.Features.Package.Commands.UpdatePackage;
using Fieldy.BookingYard.Application.Features.Package.Queries;
using Fieldy.BookingYard.Application.Features.Package.Queries.GetAllPackage;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/package")]
	[ApiController]
	public class PackageController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PackageController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreatePackage(
						[FromBody] CreatePackageCommand command,
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
		public async Task<IActionResult> UpdatePackage(
						[FromBody] UpdatePackageCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		/*[HttpPut("{id}")]
		[Route("Delete")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeletePackage(
						[FromRoute] Guid id,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new UpdatePackageCommand { PackageId = id }, cancellationToken);
			return Ok(result);
		}*/

		[HttpPost()]
		[Route("Paging")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PackageDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllPackage(
						[FromBody] RequestParams requestParams,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllPackageQuery(requestParams), cancellationToken);
			return Ok(result);
		}
	}
}
