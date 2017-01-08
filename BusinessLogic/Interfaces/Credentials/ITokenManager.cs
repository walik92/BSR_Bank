using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.Interfaces.Credentials
{
    public interface ITokenManager
    {
        Task<TokenModel> Build(CredentialModel credential);
    }
}