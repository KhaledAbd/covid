using System;
using System.ComponentModel.DataAnnotations;

namespace Coid.API.Models
{
    public class Coron
    {
        [Key]
        public int id {get; set;}
        public string Confirmed{get; set;}
        public int Deaths{get; set;}
        public int Recovered{get; set;}
        public int Active{get; set;}
        public DateTime Date{get; set;}
    }
}