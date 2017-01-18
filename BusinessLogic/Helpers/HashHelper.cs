using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Helpers
{
    /// <summary>
    ///     Obsługa funkcji skrótu
    /// </summary>
    public class HashHelper
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        ///     Pobierz hash łańcucha tekstowego
        /// </summary>
        /// <param name="text">Łańcuch tekstowy</param>
        /// <returns>Hash</returns>
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

        /// <summary>
        ///     Sprawdź Hash
        /// </summary>
        /// <param name="text">Łańcuch tekstowy</param>
        /// <param name="hash">Hash</param>
        /// <returns></returns>
        public static bool Compare(string text, string hash)
        {
            var originalHash = GetHash(text);
            return originalHash == hash;
        }
    }
}