namespace BusinessLogic.Interfaces.Credentials
{
    /// <summary>
    ///     Sprawdza ważność tokenu
    /// </summary>
    public interface ITokenValidator
    {
        /// <summary>
        ///     Sprawdź czy token jest prawidłowy i ważny
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsValid(string token);

        /// <summary>
        ///     Pobierz czas życia tokenu
        /// </summary>
        /// <returns></returns>
        int GetTokenTimeToLive();
    }
}