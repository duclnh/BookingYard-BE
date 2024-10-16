using Fieldy.BookingYard.Application.Features.Dashboard.Queries;
using Fieldy.BookingYard.Application.Features.Dashboard.Queries.GetRevenueCourtOwner;
using Fieldy.BookingYard.Application.Features.Package.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/dashboard")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DashboardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("revenue")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PackageDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetRevenue([FromQuery] GetRevenueQuery command,
														CancellationToken cancellationToken = default)
		{
			var getRevenue = await _mediator.Send(command, cancellationToken);
			return Ok(getRevenue);
		}

		[HttpGet("revenue/court-owner")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(GetRevenueCourtOwnerQuery), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetRevenueCourtOwner([FromQuery] GetRevenueCourtOwnerQuery command,
														CancellationToken cancellationToken = default)
		{
			var getRevenue = await _mediator.Send(command, cancellationToken);
			return Ok(getRevenue);
		}
	}
}
