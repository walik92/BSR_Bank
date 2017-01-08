using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Business.Credentials
{
    public class Hash
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        public static string GetHash(string text)
        {
            return HashText(text, new SHA256Managed(), Encoding);
        }

        private static string HashText(string text, HashAlgorithm algorithm, Encoding encoding = null)
        {
            var message = encoding == null ? Encoding.GetBytes(text) : encoding.GetBytes(text);
            var hashValue = algorithm.ComputeHash(message);
            return hashValue.Aggregate(string.Empty, (current, x) => current + string.Format($"{x:x2}"));
        }

        public static bool Compare(string original, string hash)
        {
            var originalHash = GetHash(original);
            return originalHash == hash;
        }
    }
}