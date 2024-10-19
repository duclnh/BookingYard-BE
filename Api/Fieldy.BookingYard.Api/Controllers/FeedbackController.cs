using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedbackCourtOwner;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedbackAdmin;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedbackFacility;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetFeedbackHome;
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
			[FromForm] CreateFeedbackCommand command,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Created(string.Empty, result);
		}

		[HttpPost("/api/feedback-owner")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateFeedbackOwnerCourt(
			[FromBody] CreateFeedbackCourtOwnerCommand command,
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

		[HttpGet("/api/feedback-facility-owner/{id}")]
		[AllowAnonymous]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "CourtOwner")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<FeedbackDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllFeedback(
			[FromRoute] Guid id,
			[FromQuery] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllFeedbackQuery(requestParams, id), cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("/api/feedback-facility/{id}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<FeedbackFacilityDetailDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetFeedbackFacility(
			[FromRoute] Guid id,
			[FromQuery] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllFeedbackFacilityQuery(requestParams, id), cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("/api/feedback-home")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(IList<FeedbackHomeDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetFeedBackHome(CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetFeedbackHomeQuery(), cancellationToken);
			return Ok(result);
		}

		[HttpGet]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<FeedbackFacilityDetailDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllFeedbackAdmin(
			[FromQuery] RequestParams requestParams,
			[FromQuery] string? orderBy,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllFeedbackAdminQuery(requestParams, orderBy), cancellationToken);
			return Ok(result);
		}
	}
}
