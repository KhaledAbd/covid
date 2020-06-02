using System;
using Coid.API.Models;

namespace Coid.API.Dtos
{
    public class UserForDetailsDto
    {
        public int Id { get; set; } 

        public string Username { get; set; }        
        public TypePerson typePerson {get; set;}

        public string PhotoUrl {get; set;} 

        public string Gender{set; get;}

        public int age {get; set;}

        public string City { get; set; }  

        public string Token {get; set;} 
    }
}