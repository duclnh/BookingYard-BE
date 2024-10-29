
using System.DrawingCore;
using System.DrawingCore.Imaging;
using Fieldy.BookingYard.Application.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fieldy.BookingYard.Infrastructure.Utility
{
    public class UtilityService : IUtilityService
    {
        private readonly ILogger<UtilityService> _logger;
        private readonly IWebHostEnvironment _environment;

        public UtilityService(IWebHostEnvironment environment, ILogger<UtilityService> logger)
        {
            _environment = environment;
            _logger = logger;
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
                var path = Path.Combine(_environment.WebRootPath, address);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        public string CreateQrCode(string paymentCode, string name, string email, string phone)
        {
            var logoPath = Path.Combine(_environment.WebRootPath, "logo.png");
            Bitmap logo = new Bitmap(logoPath);
            var qrGenerator = new QRCoder.QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(
                JsonConvert.SerializeObject(new
                {
                    paymentCode,
                    name,
                    email,
                    phone,
                }, Formatting.Indented),
                QRCoder.QRCodeGenerator.ECCLevel.Q
             );
            var qrCode = new QRCoder.QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(10, Color.Black, Color.White, logo, 25);

            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Png);
            byte[] qrCodeBytes = ms.ToArray();

            var imageBase64 = Convert.ToBase64String(qrCodeBytes);
            return $"data:image/png;base64,{imageBase64}";
        }

    }
}
