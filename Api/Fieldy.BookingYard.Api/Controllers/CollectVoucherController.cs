﻿using Fieldy.BookingYard.Application.Features.Voucher.Command.CreateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Queries;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.CreateCollectVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.UpdateCollectVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries.GetAllVoucher;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CollectVoucherController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CollectVoucherController(IMediator mediator)
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

		[HttpGet("{userId}")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(CollectVoucherDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllVoucher(
						[FromQuery] RequestParams requestParams,
						[FromRoute] Guid userId,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllCollectVoucherQuery(requestParams, userId), cancellationToken);
			return Ok(result);
		}
	}
}