using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Infrastructure.Utility
{
    public class UtilityService : IUtilityService
    {
        private readonly IWebHostEnvironment _environment;

        public UtilityService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GenerationCode()
        {
            const string chars = "0123456789";
            Random random = new();
            string randomCode = "";
            for (int i = 0; i < 6; i++)
            {
                randomCode += chars[random.Next(0, chars.Length - 1)];
            }

            return randomCode;
        }

        public string Hash(string content)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(content, 10);
        }

        public bool Verify(string content, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(content, hash);
        }

        public async Task<string> AddFile(IFormFile fileUpload, string folder)
        {
            var extension = Path.GetExtension(fileUpload.FileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine(_environment.WebRootPath, folder);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fullPath = Path.Combine(_environment.WebRootPath, folder, newFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await fileUpload.CopyToAsync(fileStream);
            }

            return $"/{folder}/{newFileName}";
        }

        public void RemoveFile(List<string> addressFile)
        {
            foreach (var address in addressFile)
            {
                if (Directory.Exists(address))
                {
                    Directory.Delete(address);
                }
            }
        }
    }
}