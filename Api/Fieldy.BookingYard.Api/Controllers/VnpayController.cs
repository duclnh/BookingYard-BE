using Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment;
using Fieldy.BookingYard.Application.Features.Payment.Dtos;
using Fieldy.BookingYard.Application.Models.Payment;
using Fieldy.BookingYard.Application.Models.Vnpay;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
												[FromQuery] TypePayment? typePayment)
		{
			var result = await _mediator.Send(new CreatePaymentCommand(typePayment));
			return Ok(result);
		}
	}
}
