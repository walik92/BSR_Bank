using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.Interfaces.Credentials
{
    /// <summary>
    ///     Tworzy token
    /// </summary>
    public interface ITokenBuilder
    {
        /// <summary>
        ///     Pobierz token
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task<TokenModel> Build(CredentialModel credential);
    }
}