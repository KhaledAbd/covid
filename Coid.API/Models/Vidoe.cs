
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Coid.API.Models{

    public class Vidoe{
        public int Id {get; set;}

        public string Url {get; set;}

        public string Description {get; set;}

        [ForeignKey("UserNavigation")]
        public int UserId {get; set;}
        [JsonIgnore]
        public User UserNavigation{get; set;}

    }
}