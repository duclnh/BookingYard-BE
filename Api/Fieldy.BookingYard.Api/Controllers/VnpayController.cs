using Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment;
using Fieldy.BookingYard.Infrastructure.Vnpay.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

		[HttpPost]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Create([FromBody] CreatePaymentCommand request,
													CancellationToken cancellationToken = default)
		{
			var result = await _mediator.Send(request, cancellationToken);
			return Ok(result);
		}

		[HttpGet]
		[Route("vnpay-return")]
		public async Task<IActionResult> VnpayReturn([FromQuery] VnpayPayResponse response)
		{
			throw new NotImplementedException();
		}
	}
}
