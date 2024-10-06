using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking
{
	public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IBookingRepository _bookingRepository;
		private readonly IHistoryPointRepository _historyPointRepository;
		private readonly ICollectVoucherRepository _collectVoucherRepository;
		private readonly IVnpayService _vnpayService;

		public CreateBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository, IHistoryPointRepository historyPointRepository, ICollectVoucherRepository collectVoucherRepository, IVnpayService vnpayService)
		{
			_mapper = mapper;
			_bookingRepository = bookingRepository;
			_historyPointRepository = historyPointRepository;
			_collectVoucherRepository = collectVoucherRepository;
			_vnpayService = vnpayService;
		}

		public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateBookingCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register booking", validationResult);

			var booking = _mapper.Map<Domain.Entities.Booking>(request);
			booking.CreatedAt = DateTime.Now;
			booking.CreatedBy = request.UserID;
			booking.ModifiedAt = DateTime.Now;
			booking.ModifiedBy = request.UserID;

			var historyPoint = await _historyPointRepository.Find(x => x.UserID == request.UserID && x.Point > 0, cancellationToken);
			if (historyPoint != null)
			{
				booking.TotalPrice = request.TotalPrice - historyPoint.Point;

				// Update user points
				historyPoint.Point = Math.Max(0, historyPoint.Point - request.TotalPrice);
				_historyPointRepository.Update(historyPoint);
				if (await _historyPointRepository.UnitOfWork.SaveChangesAsync(cancellationToken) <= 0)
				{
					return new BadRequestException("Cannot use point").Message;
				}
			}

			var collectVoucher = await _collectVoucherRepository.Find(expression: x => x.UserID == request.UserID && x.VoucherID == request.VoucherID, 
																		cancellationToken: cancellationToken,
																		includes: new Expression<Func<Domain.Entities.CollectVoucher, object>>[]
																		{
																			x => x.Voucher
																		});
			if (collectVoucher != null && booking.TotalPrice > 0)
			{
				booking.TotalPrice *= (1 - collectVoucher.Voucher.Percentage);

				// Mark voucher as used
				collectVoucher.IsUsed = true;
				_collectVoucherRepository.Update(collectVoucher);

				if (await _collectVoucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken) <= 0)
				{
					return new BadRequestException("Voucher cannot be used").Message;
				}
			}

			booking.TotalPrice = Math.Max(0, booking.TotalPrice);

			DateTime requestDate = DateTime.Now;
			booking.PaymentCode = "FIELDY" + requestDate.ToString("yyyyMMddHHmmss");

			if (booking == null)
				throw new BadRequestException("Error create booking!");

			await _bookingRepository.AddAsync(booking);

			var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result <= 0)
				throw new BadRequestException("Create new booking fail!");

			string? paymentUrl = null;
			switch (booking.PaymentMethod)
			{
				case TypePayment.VnPay:
					return _vnpayService.CreateRequestUrl(booking.TotalPrice, booking.PaymentCode, requestDate);
				case TypePayment.Momo:
					break;
				case TypePayment.PayPal:
					break;
				case TypePayment.ZaloPay:
					break;
				default:
					throw new BadRequestException("Unsupported payment type");
			}

			if (string.IsNullOrEmpty(paymentUrl))
			{
				throw new BadRequestException("Payment URL could not be created");
			}

			return paymentUrl;
		}
	}
}
