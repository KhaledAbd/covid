using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coid.API.Dtos
{
    public class CoronaForApiDto
    {
        
        public string Country {get; set;}
        
        public string CountryCode{set; get;}

        public string  Province{get; set;}

        public string City {get; set;}
        
        public string CityCode{get; set;}
        public string Lat {get; set;}
        public string  Lon {get; set;}
        public string Confirmed{get; set;}
        public int Deaths{get; set;}
        public int Recovered{get; set;}
        public int Active{get; set;}
        public DateTime Date{get; set;}
        
    }

    public class ObjData {
        public Coid.API.Dtos.Data []data {get; set;}
    }
    public class Data {
        public DateTime  date{set; get;}
        public int confirmed {get; set;}
        public int recovered_diff {get; set;}
        public int deaths_diff {get; set;}
        public int confirmed_diff {get; set;}
        public int recovered {get; set;}
        public int deaths {get; set;}

        public DateTime last_update{get; set;}

        public int active {get; set;}

        public int active_diff {get; set;}
        
        public double fatality_rate {get; set;}

        public Region region {get; set;}
    }

    public class Region {
        public string iso{set; get;}
        public string name{set; get;}
        public string province{set; get;}
        public string lat{set; get;}
        [JsonProperty("long")]
        public string longAtr{set; get;}
        public string [] cities{get; set;}
    }
}