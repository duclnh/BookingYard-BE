﻿using Fieldy.BookingYard.Application.Features.Voucher.Command.CreateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Queries;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucher;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucherFacility;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.GetVoucherHome;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

		[AllowAnonymous]
		[HttpGet("/api/voucher-home")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(IList<VoucherHomeDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetVoucherHome(CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetVoucherHomeQuery(), cancellationToken);
			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("/api/voucher-facility/{id}")]
		// [Authorize(AuthenticationSchemes = "Bearer", Roles = "CourtOwner,StaffCourt")]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(VoucherDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllFacility(
						[FromRoute] Guid id,
						[FromQuery] RequestParams requestParams,
							CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(new GetAllVoucherFacilityQuery(requestParams, id), cancellationToken);
			return Ok(result);
		}
	}
}
