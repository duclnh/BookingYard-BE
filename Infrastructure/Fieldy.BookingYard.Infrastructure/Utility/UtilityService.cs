
using SixLabors.ImageSharp;

using Fieldy.BookingYard.Application.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCoder;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

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
            var base64String = "";
            var logoPath = Path.Combine(_environment.WebRootPath, "logo.png");
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(JsonConvert.SerializeObject(new
            {
                paymentCode,
                name,
                email,
                phone,
            }, Formatting.Indented), QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new PngByteQRCode(qrCodeData))
            {
                // Generate the QR code as a byte array
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                using (var qrCodeImageStream = new MemoryStream(qrCodeImage))
                using (var qrCodeImageSharp = Image.Load<Rgba32>(qrCodeImageStream))
                {
                    // Load the logo
                    using (var logo = Image.Load<Rgba32>(logoPath))
                    {
                        // Resize logo to fit in the QR code
                        logo.Mutate(x => x.Resize(new Size(60, 60))); // Resize as needed

                        // Calculate position to place logo at the center
                        var logoX = (qrCodeImageSharp.Width - logo.Width) / 2;
                        var logoY = (qrCodeImageSharp.Height - logo.Height) / 2;

                        // Draw the logo on top of the QR code
                        qrCodeImageSharp.Mutate(x => x.DrawImage(logo, new Point(logoX, logoY), 1f));
                    }

                    // Save the final QR code with logo as a byte array
                    using (var finalImageStream = new MemoryStream())
                    {
                        qrCodeImageSharp.SaveAsPng(finalImageStream);
                        var finalImageBytes = finalImageStream.ToArray();

                        // Convert to Base64 string for JSON response
                        base64String = Convert.ToBase64String(finalImageBytes);

                    }
                }
            }
            return $"data:image/png;base64,{base64String}";
        }

    }
}
