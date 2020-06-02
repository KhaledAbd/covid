using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Coid.API.Data;
using Coid.API.Dtos;
using Coid.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coid.API.Controllers
{
    [Route("/api/corona")]
    [ApiController]
    public class CoronController: ControllerBase
    {
        private readonly IDataRepository repository;

        public CoronController (IDataRepository repository){
            this.repository = repository;
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> getCoronaVirus(){
            if(DateTime.Now.Hour < 18 && DateTime.Now.Hour < 22){
                await ReedResultServer();
            }

            return Ok(await repository.GetCorons());
        }

        private async Task ReedResultServer(){
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
            if(dataObj.data.Length == 0)
                return;
            if(dataObj.data[0] != null && !await repository.AnyCoron()){
                await repository.Add(new Coron {
                    Active = dataObj.data[0].active,
                    Confirmed = dataObj.data[0].confirmed.ToString(),
                    Date = dataObj.data[0].date,
                    Deaths = dataObj.data[0].deaths,
                    Recovered = dataObj.data[0].recovered
                });
                await repository.Save();
            }
        }

    }
}