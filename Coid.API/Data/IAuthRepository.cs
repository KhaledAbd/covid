using System.Threading.Tasks;
using Coid.API.Models;

namespace Coid.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);

        Task<User> Register(User user, string password);

        Task<bool> UserExists(string username);
    }
}