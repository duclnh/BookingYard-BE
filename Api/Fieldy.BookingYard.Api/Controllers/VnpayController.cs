using Azure.Core;
using Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment;
using Fieldy.BookingYard.Application.Features.Payment.Queries;
using Fieldy.BookingYard.Infrastructure.Vnpay.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;

namespace Fieldy.BookingYard.Api.Controllers
{
	[Route("api/vnpay")]
	[ApiController]
	public class VnpayController : ControllerBase
	{
		private readonly IMediator _mediator;

		public VnpayController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("vnpay-return")]
		public async Task<IActionResult> VnpayReturn([FromQuery] GetVnpayReturnQuery response,
													CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(response, cancellationToken);
			return Ok(result);
		}
	}
}
