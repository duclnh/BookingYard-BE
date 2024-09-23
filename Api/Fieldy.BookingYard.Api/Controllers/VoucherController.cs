using Fieldy.BookingYard.Application.Features.Voucher.Command.CreateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Queries;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucher;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/voucher")]
	[ApiController]
	public class VoucherController : ControllerBase
	{
		private readonly IMediator _mediator;

		public VoucherController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateVoucher(
						[FromForm] CreateVoucherCommand command,
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
		public async Task<IActionResult> UpdateVoucher(
						[FromBody] UpdateVoucherCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[HttpGet]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(VoucherDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllVoucher(
						[FromQuery] RequestParams requestParams,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllVoucherQuery(requestParams), cancellationToken);
			return Ok(result);
		}
	}
}
