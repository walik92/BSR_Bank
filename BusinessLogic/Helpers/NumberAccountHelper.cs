using System;
using System.Text.RegularExpressions;

namespace BusinessLogic.Helpers
{
    /// <summary>
    ///     Pomocnicze operacje wykonywane na numerze konta
    /// </summary>
    public class NumberAccountHelper
    {
        private static readonly int _mybankCode = 129725;

        /// <summary>
        ///     Wylicz sumę kontrolną numeru konta
        /// </summary>
        /// <param name="id">Identyfikator numeru konta</param>
        /// <returns>Suma kontrolna</returns>
        public static byte CalculateChecksum(long id)
        {
            var number = _mybankCode.ToString().PadLeft(8, '0') + id.ToString().PadLeft(16, '0') + "252100";
            if (string.IsNullOrEmpty(number))
                throw new ArgumentException("The Id can't be empty.");

            if (!Regex.IsMatch(number, @"^\d{30}$"))
                throw new ArgumentException("The account number is too short.");
            var modulo = 0;
            foreach (var znak in number)
                modulo = (10 * modulo + int.Parse(znak.ToString())) % 97;
            modulo = 98 - modulo;

            return (byte) modulo;
        }

        /// <summary>
        ///     Pobierz pełen numer konta bankowego
        /// </summary>
        /// <param name="checksum">Suma kontrolna</param>
        /// <param name="id">Identyfikator konta</param>
        /// <returns>Numer konta bankowego</returns>
        public static string GetFullNumberAccount(byte checksum, long id)
        {
            var number = _mybankCode.ToString().PadLeft(8, '0') + id.ToString().PadLeft(16, '0');
            var splitNumber = checksum.ToString().PadLeft(2, '0') + " " + string.Join(" ",
                                  Regex.Split(number, "(....)(....)(....)(....)(....)(....)")).Trim();

            return splitNumber;
        }

        /// <summary>
        ///     Sprawdź czy konto należy do mojego banku
        /// </summary>
        /// <param name="number">Numer konta bankowego</param>
        /// <returns></returns>
        public static bool IsAccountInMyBank(string number)
        {
            return ExtractIdBank(number) == _mybankCode;
        }

        /// <summary>
        ///     Pobiera identyfikator numeru konta w db
        /// </summary>
        /// <param name="number">Numer konta</param>
        /// <returns>Identyfikator</returns>
        public static long GetNumberAccount(string number)
        {
            number = number.Replace(" ", null);
            var num = number.Remove(0, 10);
            return long.Parse(num);
        }

        /// <summary>
        ///     Pobierz sumę kontrolną z numeru konta
        /// </summary>
        /// <param name="number">Numer konta</param>
        /// <returns>Suma kontrolna</returns>
        public static byte GetChecksum(string number)
        {
            number = number.Replace(" ", null);
            var num = number.Substring(0, 2);
            return byte.Parse(num);
        }

        /// <summary>
        ///     Pobierz Identyfikator banku
        /// </summary>
        /// <param name="number">Numer konta bankowego</param>
        /// <returns>Identyfikator banku</returns>
        public static int ExtractIdBank(string number)
        {
            number = number.Replace(" ", null);
            return int.Parse(number.Replace(" ", "").Substring(2, 8).TrimStart('0'));
        }

        /// <summary>
        ///     Porównaj numery kont
        /// </summary>
        /// <param name="number1">Numer konta pierwszy</param>
        /// <param name="number2">Numer konta drugi</param>
        /// <returns></returns>
        public static bool EqualsNumbers(string number1, string number2)
        {
            number1 = number1.Replace(" ", null);
            number2 = number2.Replace(" ", null);
            return number1 == number2;
        }

        /// <summary>
        ///     Usuń białe znaki z numeru konta
        /// </summary>
        /// <param name="number">Numer konta</param>
        /// <returns></returns>
        public static string ClearNumber(string number)
        {
            return number.Replace(" ", null);
        }

        /// <summary>
        ///     Pobierz sformatowany numer konta
        /// </summary>
        /// <param name="number">Numer konta</param>
        /// <returns>Sformatowany numer konta</returns>
        public static string FormatNumber(string number)
        {
            return string.Join(" ", Regex.Split(number, "(..)(....)(....)(....)(....)(....)(....)")).Trim();
        }
    }
}