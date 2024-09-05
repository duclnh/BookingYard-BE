using Fieldy.BookingYard.Application.Contracts;

namespace Fieldy.BookingYard.Infrastructure.Utility
{
    public class UtilityService : IUtilityService
    {
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
    }
}