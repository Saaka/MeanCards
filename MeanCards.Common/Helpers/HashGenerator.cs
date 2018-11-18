using System.Security.Cryptography;
using System.Text;

namespace MeanCards.Common.Helpers
{
    public class HashGenerator
    {
        public string Generate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.Default.GetBytes(email));
                var stringBuilder = new StringBuilder();
                for(int i  = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
