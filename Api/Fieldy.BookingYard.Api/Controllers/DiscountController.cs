using Fieldy.BookingYard.Application.Features.Discount.Command.CreateDiscount;
using Fieldy.BookingYard.Application.Features.Discount.Command.UpdateDiscount;
using Fieldy.BookingYard.Application.Features.Discount.Queries;
using Fieldy.BookingYard.Application.Features.Discount.Queries.GetAllDiscount;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiscountController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DiscountController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateDiscount(
						[FromBody] CreateDiscountCommand command,
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
		public async Task<IActionResult> UpdateDiscount(
						[FromBody] UpdateDiscountCommand command,
									CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[HttpPost()]
		[Route("Paging")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(DiscountDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllDiscount(
						[FromBody] RequestParams requestParams,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllDiscountQuery(requestParams), cancellationToken);
			return Ok(result);
		}
	}
}
