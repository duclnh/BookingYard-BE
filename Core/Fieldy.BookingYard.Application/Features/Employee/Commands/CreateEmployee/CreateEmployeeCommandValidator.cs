using Fieldy.BookingYard.Application.Features.Employee.Commands.CreateEmployee;
using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Employee.CreateEmployee{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>{
        public CreateEmployeeCommandValidator()
        {
            
        }
    }
}