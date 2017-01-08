using BusinessLogic.Interfaces.Credentials;
using BusinessLogic.Model;
using RepozytoriumDB.DTO;

namespace BusinessLogic.Business.Credentials
{
    public class CredentialsValidator : ICredentialsValidator
    {
        public bool IsValid(CredentialModel credential, Client client)
        {
            return Hash.Compare(credential.Password, client.Password);
        }
    }
}