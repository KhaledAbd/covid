using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using AutoMapper;
using Coid.API.Data;
using Coid.API.Dtos;
using Coid.API.Models;
using Newtonsoft.Json;

namespace Coid.API.seed
{
    public class CoronaSeed
    {
        private readonly IMapper mapper;
        private readonly ContextData context;

        public CoronaSeed(IMapper mapper, ContextData context){
            this.mapper = mapper;
            this.context = context;
        } 

        public void ReedFromServer(){
            string data;
            string url = @"https://api.covid19api.com/total/country/egypt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
            List<CoronaForApiDto> listCorona = JsonConvert.DeserializeObject<List<CoronaForApiDto>>(data);
            List<Coron> corons = mapper.Map<List<Coron>>(listCorona);
            foreach(var coron in corons){
                Console.WriteLine("====>\n " + coron.Date);
            }
            context.AddRange(corons);
            context.SaveChanges();
        }
        public void ReedResultServer(){
            string data;
            string url = @"https://covid-api.com/api/reports?date="+ DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") +"&q=egypt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }

            ObjData dataObj = JsonConvert.DeserializeObject<ObjData>(data);
            if(dataObj.data[0] != null && !context.Corons.Any(c => c.Date.Equals(DateTime.Now))){
                context.Add(new Coron {
                    Active = dataObj.data[0].active,
                    Confirmed = dataObj.data[0].confirmed.ToString(),
                    Date = dataObj.data[0].date,
                    Deaths = dataObj.data[0].deaths,
                    Recovered = dataObj.data[0].recovered
                });
                context.SaveChanges();
            }
        }
        private System.Threading.Timer timer;

        public void SetUpTimer(TimeSpan alertTime)
        {
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
            {
                return;//time already passed
            }
            this.timer = new System.Threading.Timer(x =>
            {
                this.ReedResultServer();
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

    }
}