using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedBackFacility;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/feedback")]
	[Authorize]
	[ApiController]
	public class FeedbackController : ControllerBase
	{
		private readonly IMediator _mediator;

		public FeedbackController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateFeedback(
			[FromBody] CreateFeedbackCommand command,
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
		public async Task<IActionResult> UpdateFeedback(
						[FromBody] UpdateFeedbackCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpDelete]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteFeedback(
			[FromBody] DeleteFeedbackCommand command,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[HttpGet("{facilityId}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<FeedbackDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllFeedback(
			[FromRoute] Guid facilityId,
			[FromQuery] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllFeedbackQuery(requestParams, facilityId), cancellationToken);
			return Ok(result);
		}
		
		[AllowAnonymous]
		[HttpGet("/api/feedback-facility/{id}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<FeedbackFacilityDetailDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetFeedbackFacility(
			[FromRoute] Guid id,
			[FromQuery] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetFeedBackFacilityQuery(requestParams, id), cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("/api/testhost")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> TestHost(
			CancellationToken cancellationToken = default)
		{
			var host = $"{Request.Scheme}://{Request.Host}/{Request.PathBase}";
			return Ok(host);
		}
	}
}
