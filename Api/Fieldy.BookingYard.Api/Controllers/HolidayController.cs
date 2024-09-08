using Fieldy.BookingYard.Application.Features.Holiday.Commands.CreateHoliday;
using Fieldy.BookingYard.Application.Features.Holiday.Commands.DeleteHoliday;
using Fieldy.BookingYard.Application.Features.Holiday.Commands.UpdateHoliday;
using Fieldy.BookingYard.Application.Features.Holiday.Queries;
using Fieldy.BookingYard.Application.Features.Holiday.Queries.GetAllHoliday;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/holiday")]
	[Authorize]
	[ApiController]
	public class HolidayController : ControllerBase
	{
		private readonly IMediator _mediator;

		public HolidayController(IMediator mediator)
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
		public async Task<IActionResult> CreateHoliday(
			[FromBody] CreateHolidayCommand command,
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
		public async Task<IActionResult> UpdateHoliday(
						[FromBody] UpdateHolidayCommand command,
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
		public async Task<IActionResult> DeleteHoliday(
			[FromBody] DeleteHolidayCommand command,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpPost()]
		[Route("Paging")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(PagingResult<HolidayDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllHoliday(
			[FromBody] RequestParams requestParams,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllHolidayQuery(requestParams), cancellationToken);
			return Ok(result);
		}
	}
}
