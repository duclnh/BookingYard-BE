using Microsoft.AspNetCore.Mvc;

namespace Fiedly.BookingYard.Api.Models{
    public class CustomProblemDetails : ProblemDetails{
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(); 
    }
}