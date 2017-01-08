using System;
using System.Text.RegularExpressions;

namespace BusinessLogic.Business.Account
{
    public class NumberAccountManager
    {
        private static readonly int _bankCode = 129725;

        public static byte CalculateChecksum(long id)
        {
            var number = _bankCode.ToString().PadLeft(8, '0') + id.ToString().PadLeft(16, '0') + "252100";
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

        public static string GetFullNumberAccount(byte checksum, long id)
        {
            var number = _bankCode.ToString().PadLeft(8, '0') + id.ToString().PadLeft(16, '0');
            var splitNumber = checksum.ToString().PadLeft(2, '0') + " " + string.Join(" ",
                                  Regex.Split(number, "(....)(....)(....)(....)(....)(....)")).Trim();

            return splitNumber;
        }


        public static bool IsAccountInMyBank(string number)
        {
            return ExtractIdBank(number) == _bankCode;
        }

        //pobiera identyfikator numeru konta w db
        public static long GetNumberAccount(string number)
        {
            number = number.Replace(" ", null);
            var num = number.Remove(0, 10);
            return long.Parse(num);
        }

        public static byte GetChecksum(string number)
        {
            number = number.Replace(" ", null);
            var num = number.Substring(0, 2);
            return byte.Parse(num);
        }

        public static int ExtractIdBank(string number)
        {
            number = number.Replace(" ", null);
            return int.Parse(number.Replace(" ", "").Substring(2, 8).TrimStart('0'));
        }


        public static bool EqualsNumbers(string number1, string number2)
        {
            number1 = number1.Replace(" ", null);
            number2 = number2.Replace(" ", null);
            return number1 == number2;
        }

        public static string ClearNumber(string number)
        {
            return number.Replace(" ", null);
        }

        public static string FormatNumber(string number)
        {
            return string.Join(" ", Regex.Split(number, "(..)(....)(....)(....)(....)(....)(....)")).Trim();
        }
    }
}