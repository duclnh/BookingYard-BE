using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Queries
{
	public class GetVnpayReturnQueryHandler : IRequestHandler<GetVnpayReturnQuery, Guid>
	{
		private readonly IVnpayService _vnpayService;
		private readonly IBookingRepository _bookingRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly IMapper _mapper;
		public GetVnpayReturnQueryHandler(IVnpayService vnpayService, IBookingRepository bookingRepository, ITransactionRepository transactionRepository, IMapper mapper)
		{
			_vnpayService = vnpayService;
			_bookingRepository = bookingRepository;
			_transactionRepository = transactionRepository;
			_mapper = mapper;
		}

		public async Task<Guid> Handle(GetVnpayReturnQuery request, CancellationToken cancellationToken)
		{
			var booking = await _bookingRepository.Find(x => x.PaymentCode == request.vnp_OrderInfo);
			if (booking == null)
			{
				throw new NotFoundException(nameof(Booking), request.vnp_OrderInfo);
			}
			if (request.vnp_TransactionStatus == "00")
			{
				//Update booking with payment status
				booking.PaymentStatus = true;
				_bookingRepository.Update(booking);
				var saveBooking = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
				if (saveBooking < 0)
				{
					throw new BadRequestException("Update booking fail");
				}
			}
			else
			{
				_bookingRepository.Remove(booking);
				await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
				throw new BadRequestException("Payment fail");
			}
			//Create transaction
			// var trans = _mapper.Map<Transaction>(request);
			// if (trans != null)
			// {
			// 	await _transactionRepository.AddAsync(trans);
			// }

			// var saveTransaction = await _transactionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			// if (saveTransaction < 0)
			// {
			// 	throw new BadRequestException("Create transaction fail");
			// }

			// if (request.vnp_TransactionStatus == "01")
			// {
			// 	return "Payment in progress";
			// }
			// else if (request.vnp_TransactionStatus == "02")
			// {
			// 	return "Payment process failed";
			// }
			// else if (request.vnp_TransactionStatus == "03")
			// {
			// 	return "Payment is cancelled";
			// }
			return booking.Id;
		}
	}
}
