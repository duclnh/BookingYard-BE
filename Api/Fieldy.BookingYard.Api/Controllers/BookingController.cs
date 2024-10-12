using Fieldy.BookingYard.Application.Features.Booking.Commands.CancelBooking;
using Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingCustomer;
using Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingFacility;
using Fieldy.BookingYard.Application.Features.Booking.Queries.GetBookingDetail;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/booking")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BookingController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateBooking(
		   [FromBody] CreateBookingCommand command,
		   CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Created("", result);
		}

		[AllowAnonymous]
		[HttpGet("detail/{id}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(FacilityDetailDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetBookingDetail(
				[FromRoute] Guid id,
				CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetBookingDetailQuery(id, cancellationToken));
			return Ok(result);
		}

		[HttpGet("{customerId}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(IList<BookingDetailDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllBookingCustomer(
				[FromQuery] RequestParams requestParams,
				[FromRoute] Guid customerId,
				[FromQuery] string type,
				CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllBookingCustomerQuery(requestParams, customerId, type, cancellationToken));
			return Ok(result);
		}

		[HttpGet("facility/{id}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(IList<BookingDetailDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllBookingFacility(
				[FromQuery] RequestParams requestParams,
				[FromRoute] Guid id,
				[FromQuery] string? SportID,
				[FromQuery] string? Status,
				CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllBookingFacilityQuery(requestParams, id, SportID, Status, cancellationToken));
			return Ok(result);
		}

		[HttpPost("/api/cancel-booking")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateBooking(
			[FromBody] CancelBookingCommand command,
			CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}
	}
}
