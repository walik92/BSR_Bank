using BusinessLogic.Model;
using RepozytoriumDB.DTO;

namespace BusinessLogic.Interfaces.Credentials
{
    public interface ICredentialsValidator
    {
        bool IsValid(CredentialModel credential, Client client);
    }
}