using System.ComponentModel.DataAnnotations;

namespace Coid.API.Dtos
{
    public class UserForLoginDto
    {
        public string username {get; set;}

        public string password {get; set;}
    }
}