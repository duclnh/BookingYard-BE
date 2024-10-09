using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.CreateCollectVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.UpdateCollectVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries.GetAllVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/collect-voucher")]
	[ApiController]
	[Authorize]
	public class CollectVoucherController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CollectVoucherController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateVoucher(
						[FromBody] CreateCollectVoucherCommand command,
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
						[FromBody] UpdateCollectVoucherCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CollectVoucherDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllVoucher(
						[FromQuery] RequestParams requestParams,
						[FromRoute] Guid id,
						[FromQuery] string? type,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllCollectVoucherQuery(requestParams, id, type), cancellationToken);
			return Ok(result);
		}

	}
}
