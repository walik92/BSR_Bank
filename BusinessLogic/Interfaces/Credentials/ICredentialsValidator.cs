using BusinessLogic.Model;
using RepozytoriumDB.DTO;

namespace BusinessLogic.Interfaces.Credentials
{
    /// <summary>
    ///     Uwierzytelnianie użytkownika
    /// </summary>
    public interface ICredentialsValidator
    {
        /// <summary>
        ///     Sprawdź poprawność uwierzytelniania
        /// </summary>
        /// <param name="credential">Dane uwierzytelniania</param>
        /// <param name="client">Klient</param>
        /// <returns></returns>
        bool IsValid(CredentialModel credential, Client client);
    }
}