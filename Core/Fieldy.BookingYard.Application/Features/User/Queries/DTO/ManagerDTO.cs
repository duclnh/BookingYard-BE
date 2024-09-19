using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldy.BookingYard.Application.Features.User.Queries.DTO
{
    public class ManagerDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Gender { get; set; }
        public int? WardID { get; set; }
        public required string Role { get; set; }
        public string? FacilityName { get; set; }
        public string? FacilityImage { get; set; }
        public Guid? FacilityID { get; set; }
    }
}
