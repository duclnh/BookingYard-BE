using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
	{
		private readonly IVnpayService _paymentService;
		private readonly IBookingRepository _bookingRepository;

		public CreatePaymentCommandHandler(IVnpayService paymentService, IBookingRepository bookingRepository)
		{
			_paymentService = paymentService;
			_bookingRepository = bookingRepository;
		}

		public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var booking = await _bookingRepository.FindByIdAsync(request.BookingId, cancellationToken);
				if (booking == null)
				{
					throw new NotFoundException(nameof(Booking), request.BookingId);
				}
				if (booking.Status == true)
				{
					throw new BadRequestException("Booking has been paid");
				}
				if (booking.PaymentCode != null)
				{
					return _paymentService.CreateRequestUrl(request.RequiredAmount, booking.PaymentCode, DateTime.Now);
				}

				var requestTime = DateTime.Now;
				string formattedDateTime24Hour = requestTime.ToString("yyyyMMddHHmmss");
				string paymentCode = "FIELDY" + formattedDateTime24Hour;

				//Update booking with payment code
				booking.PaymentCode = paymentCode;
				_bookingRepository.Update(booking);
				return _paymentService.CreateRequestUrl(request.RequiredAmount, paymentCode, requestTime);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred in vnpay payment: {ex.Message}");
				throw;
			}
		}
	}
}
