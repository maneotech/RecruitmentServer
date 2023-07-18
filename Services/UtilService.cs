using System.Security.Cryptography;
using System.Text;

namespace RecruitmentServer.Services
{
    public class UtilService
    {
        public static int GetPositionOfNextSpace(string text, int start) 
        {
            char currentChar = 'x';

            while (currentChar != ' ')
            {
                currentChar = text[start];
                start++;
            }

            return start - 1;
        }

        public static int GetPositionOfNextChar(string text, int start)
        {
            char currentChar = ' ';

            while (currentChar == ' ')
            {
                currentChar = text[start];
                start++;
            }

            return start - 1;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(bytes);
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }

    }
}
