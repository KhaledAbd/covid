using System.Threading.Tasks;
using Coid.API.Data;
using Coid.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coid.API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController
    {
        private readonly IAuthRepository repository;

        public UserController(IAuthRepository repository){
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password){
            User user =  await repository.Login(username, password);
            return null;

        }
    }
}