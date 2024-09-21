using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.HistoryPoint.Queries;
using Fieldy.BookingYard.Application.Features.HistoryPoint.Queries.GetHistoryPoint;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/historypoint")]
	[ApiController]
	public class HistoryPointController : ControllerBase
	{
		private readonly IMediator _mediator;

		public HistoryPointController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[AllowAnonymous]
		[HttpGet("{userId}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<HistoryPointDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetPointByUserID(
			[FromRoute] Guid userId,
			[FromQuery] RequestParams requestParams,
			[FromQuery] string type,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetHistoryPointQuery(requestParams, userId, type), cancellationToken);
			return Ok(result);
		}
	}
}
