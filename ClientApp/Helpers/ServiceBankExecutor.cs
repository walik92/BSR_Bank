using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.ServiceBankReference;

namespace ClientApp.Helpers
{
    /// <summary>
    ///     Wykonywanie operacji z usługi bankowej
    /// </summary>
    public static class ServiceBankExecutor
    {
        private static Token _token;
        private static readonly ServiceBankClient ServiceBank = new ServiceBankClient();

        /// <summary>
        ///     Zaloguj się
        /// </summary>
        /// <param name="id">Id Klienta</param>
        /// <param name="password">Hasło</param>
        /// <returns></returns>
        public static async Task Login(int id, string password)
        {
            var cred = new Credential {Id = id, Password = password};

            var task = Task.Factory.StartNew(() => ServiceBank.Login(cred));
            if (task == await Task.WhenAny(task, Task.Delay(10000)))
                _token = await task;
            else
                throw new Exception("Timed out");
        }

        /// <summary>
        ///     Pobierz konta
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Account>> GetAccounts()
        {
            var task = Task.Factory.StartNew(() => ServiceBank.GetAccounts(_token.Value));

            if (task == await Task.WhenAny(task, Task.Delay(10000)))
                return (await task).ToList();
            throw new Exception("Timed out");
        }

        /// <summary>
        ///     Wykonaj transfer
        /// </summary>
        /// <param name="accountFrom">Konto źródłowe</param>
        /// <param name="accountTo">Konto docelowe</param>
        /// <param name="amount">Kwota</param>
        /// <param name="title">Tytuł transferu</param>
        /// <returns></returns>
        public static async Task Transfer(string accountFrom, string accountTo, int amount, string title)
        {
            var transfer = new Transfer();
            transfer.AccountFrom = accountFrom;
            transfer.AccountTo = accountTo;
            transfer.Amount = amount;
            transfer.Title = title;

            var task = Task.Factory.StartNew(() => ServiceBank.Transfer(_token.Value, transfer));

            if (task == await Task.WhenAny(task, Task.Delay(10000)))
                await task;
            else
                throw new Exception("Timed out");
        }

        /// <summary>
        ///     Wpłać kwotę
        /// </summary>
        /// <param name="accountTo">Konto docelowe</param>
        /// <param name="amount">Kwota</param>
        /// <returns></returns>
        public static async Task PayIn(string accountTo, int amount)
        {
            var task = Task.Factory.StartNew(() => ServiceBank.PayIn(_token.Value, amount, accountTo));

            if (task == await Task.WhenAny(task, Task.Delay(10000)))
                await task;
            else
                throw new Exception("Timed out");
        }

        /// <summary>
        ///     Wypłać kwotę
        /// </summary>
        /// <param name="accountFrom"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static async Task PayOut(string accountFrom, int amount)
        {
            var task = Task.Factory.StartNew(() => ServiceBank.PayOut(_token.Value, amount, accountFrom));

            if (task == await Task.WhenAny(task, Task.Delay(10000)))
                await task;
            else
                throw new Exception("Timed out");
        }

        /// <summary>
        ///     Pobierz czas życia tokenu w sekundach
        /// </summary>
        /// <returns></returns>
        public static int GetTokenTtl()
        {
            return _token.TimeToLive;
        }

        /// <summary>
        ///     Pobierz historię konta
        /// </summary>
        /// <param name="account">Numer konta</param>
        /// <param name="currentPage">Numer strony</param>
        /// <param name="sizePage">Rozmiar strony</param>
        /// <returns></returns>
        public static async Task<HistoryOfAccount> GetHistoryOfAccount(string account, int currentPage, int sizePage)
        {
            var task =
                Task.Factory.StartNew(
                    () => ServiceBank.GetHistoryOfAccount(_token.Value, account, currentPage, sizePage));

            if (task == await Task.WhenAny(task, Task.Delay(10000)))
                return await task;

            throw new Exception("Timed out");
        }
    }
}