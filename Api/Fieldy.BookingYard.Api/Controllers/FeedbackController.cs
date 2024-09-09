using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback;
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

		[AllowAnonymous]
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

		[AllowAnonymous]
		[HttpPost()]
		[Route("Paging")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<FeedbackDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllFeedback(
			[FromBody] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllFeedbackQuery(requestParams), cancellationToken);
			return Ok(result);
		}
	}
}
