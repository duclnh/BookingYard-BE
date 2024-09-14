using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Contracts
{
    public interface IUtilityService
    {
        public string GenerationCode();
        public string Hash(string content);
        public bool Verify(string content, string hash);
        public Task<string> AddFile(IFormFile fileUpload, string folder);
    };
}