using System;
using System.Collections.Generic;

namespace Coid.API.Models
{
    public class User
    {
        public int Id { get; set; } 

        public string Username { get; set; }

        public string KnownAs {get; set;}

        public GenderType Gender{set; get;}
        
        public DateTime DateOfBirth {get; set;}
        
        public string City { get; set; }
        
        public byte[] passwordHash { get; set; }

        public byte[] passwordSalt{get; set;}
        
        public TypePerson typePerson {get; set;}

        public virtual IList<Photo> Photos {get; set;} 
       
        public virtual IList<Vidoe> Vidoes {get; set;} 

    }
}