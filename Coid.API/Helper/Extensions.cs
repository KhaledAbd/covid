using System;
using Coid.API.Models;
using Microsoft.AspNetCore.Http;

namespace Coid.API.Helper
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse http, string message){
            http.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            http.Headers.Add("Application-Error", message);
            http.Headers.Add("Access-Control-Allow-Origin", "*");

        }
        public static int CalculateAge(this DateTime dateOfBirth){
            var nowDate = DateTime.Now;
            var age = nowDate.Year - dateOfBirth.Year;
            var dateAge = new DateTime(age,nowDate.Month,nowDate.Day);
            if(dateAge > nowDate)
                age--;
            return age;
        }

        public static string GetGender(this GenderType gender){
            string genderString;
            switch (gender)
            {
                case GenderType.FAMULE:
                    genderString = "أمراه";
                    break;
                case GenderType.MAN:
                    genderString = "رجل";
                    break;
                default:
                    genderString = "";
                    break;
            }
            return genderString;
        }
    }
}