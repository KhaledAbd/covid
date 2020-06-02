using System.Linq;
using AutoMapper;
using Coid.API.Dtos;
using Coid.API.Models;

namespace Coid.API.Helper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile(){
            CreateMap<User, UserForDetailsDto>().ForMember(dest=>dest.PhotoUrl, opt =>{
                opt.MapFrom(src =>src.Photos.FirstOrDefault(p => p.IsMain).Url);
            }).ForMember(dest => dest.age, opt => {
                opt.MapFrom(src => src.DateOfBirth.CalculateAge());
            }).ForMember(dest => dest.Gender, opt => {
                opt.MapFrom(src => src.Gender.GetGender());
            });
            CreateMap<CoronaForApiDto, Coron>();
        }
    }
}