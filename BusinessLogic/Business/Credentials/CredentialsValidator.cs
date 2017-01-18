using BusinessLogic.Helpers;
using BusinessLogic.Interfaces.Credentials;
using BusinessLogic.Model;
using RepozytoriumDB.DTO;

namespace BusinessLogic.Business.Credentials
{
    /// <summary>
    ///     Uwierzytelnianie użytkownika
    /// </summary>
    public class CredentialsValidator : ICredentialsValidator
    {
        public bool IsValid(CredentialModel credential, Client client)
        {
            return HashHelper.Compare(credential.Password, client.Password);
        }
    }
}