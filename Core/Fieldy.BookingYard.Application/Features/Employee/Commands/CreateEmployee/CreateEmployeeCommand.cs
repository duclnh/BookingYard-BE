using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<string>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required Gender Gender { get; set; }
        public required Role Role { get; set; }

        public required TypeWork TypeWork { get; set; }
        public required decimal Salary { get; set; }
    }
}