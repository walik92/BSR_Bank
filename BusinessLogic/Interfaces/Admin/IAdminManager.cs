using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.Admin
{
    public interface IAdminManager
    {
        Task<int> AddClient(string password);
        Task<string> AddAccount(int clientId);
    }
}