using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Coid.API.Models
{
    public class Photo
    {
        public int Id {get; set;}

        public string Url {get; set;}

        public DateTime Created {get; set;}
        public string Description {get; set;}

        public bool IsMain {get; set;}

        public bool IsCertificated {get; set;}

        [ForeignKey("UserNavigation")]
        public int UserId {get; set;}
        [JsonIgnore]
        public User UserNavigation{get; set;}
    }
}