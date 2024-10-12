using AutoMapper;
using AutoMapper.Execution;
using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
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
		private readonly IUserRepository _userRepository;
		private readonly ICollectVoucherRepository _collectVoucherRepository;
		private readonly IVnpayService _vnpayService;
		private readonly ICourtRepository _courtRepository;

		public CreateBookingCommandHandler(IMapper mapper,
											IBookingRepository bookingRepository,
											IHistoryPointRepository historyPointRepository,
											ICollectVoucherRepository collectVoucherRepository,
											IVnpayService vnpayService,
											IUserRepository userRepository,
											ICourtRepository courtRepository)
		{
			_mapper = mapper;
			_bookingRepository = bookingRepository;
			_historyPointRepository = historyPointRepository;
			_collectVoucherRepository = collectVoucherRepository;
			_vnpayService = vnpayService;
			_userRepository = userRepository;
			_courtRepository = courtRepository;
		}

		public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateBookingCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register booking", validationResult);

			var court = await _courtRepository.FindByIdAsync(request.CourtID, cancellationToken);
			if (court == null)
				throw new NotFoundException(nameof(court), request.CourtID);

			var booking = _mapper.Map<Domain.Entities.Booking>(request);
			if (booking == null)
				throw new BadRequestException("Error create booking!");

			booking.CreatedAt = DateTime.Now;
			booking.CreatedBy = request.UserID;
			booking.ModifiedAt = DateTime.Now;
			booking.ModifiedBy = request.UserID;
			booking.IsCheckin = false;
			booking.IsFeedback = false;

			booking.TotalPrice = (booking.EndTime.Hours - booking.StartTime.Hours) * court.CourtPrice;
			booking.OwnerPrice = booking.TotalPrice;

			// Add payment code
			DateTime requestDate = DateTime.Now;
			booking.PaymentCode = "FIELDY" + requestDate.ToString("yyyyMMddHHmmss");


			#region Check voucher is available
			var collectVoucher = await _collectVoucherRepository.Find(expression: x => x.UserID == request.UserID && x.Id == request.CollectVoucherID,
																		cancellationToken: cancellationToken,
																		x => x.Voucher
																		);
			if (collectVoucher != null && collectVoucher.Voucher != null && booking.TotalPrice > 0)
			{
				var percentage = Convert.ToDecimal(collectVoucher.Voucher.Percentage);
				if (court.FacilityID == collectVoucher.Voucher.FacilityID)
				{
					booking.OwnerPrice -= booking.TotalPrice * (percentage / 100);
				}

				booking.TotalPrice -= booking.TotalPrice * (percentage / 100);

				// Mark voucher as used
				collectVoucher.IsUsed = true;
				_collectVoucherRepository.Update(collectVoucher);
				booking.VoucherID = collectVoucher.VoucherID;
			}
			#endregion

			var user = await _userRepository.FindByIdAsync(request.UserID, cancellationToken);

			if (user == null)
			{
				throw new NotFoundException(nameof(user), request.UserID);
			}

			if (request.Point > 0)
			{

				user.Point -= (int)request.Point;

				booking.TotalPrice -= (int)request.Point;

				if (user.Point < 0)
					throw new BadRequestException("Point invalid");

				booking.UsedPoint = request.Point;

				_userRepository.Update(user);

				var historyPointEntity = new Domain.Entities.HistoryPoint
				{
					Id = 0,
					UserID = booking.UserID,
					Point = -1 * (int)request.Point,
					CreatedAt = DateTime.Now,
					Content = "Đặt lịch " + booking.PaymentCode,
				};

				await _historyPointRepository.AddAsync(historyPointEntity);
			}

			// Update total price
			booking.TotalPrice = Math.Max(0, booking.TotalPrice);

			//-15% for corporation
			booking.OwnerPrice = booking.OwnerPrice * ((decimal)85 / 100);

			await _bookingRepository.AddAsync(booking);

			if (await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken) <= 0)
			{
				return new BadRequestException("Create new booking fail!").Message;
			}

			// Check payment type
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
