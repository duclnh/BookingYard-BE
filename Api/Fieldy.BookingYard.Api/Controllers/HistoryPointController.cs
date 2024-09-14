﻿using Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/[controller]")]
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
		[ProducesResponseType(typeof(PagingResult<FeedbackDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetPointByUserID(
			[FromRoute] Guid userId,
			[FromQuery] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllFeedbackQuery(requestParams, userId), cancellationToken);
			return Ok(result);
		}
	}
}
