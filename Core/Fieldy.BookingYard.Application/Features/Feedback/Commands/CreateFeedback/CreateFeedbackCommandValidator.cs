using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback
{
	public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
	{
		public CreateFeedbackCommandValidator()
		{
		}
	}
}
