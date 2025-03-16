using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using API.DTOS;
using API.Models;

namespace API.Helper
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<FutureResults, FutureResultsDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Gender_Female, opt => opt.MapFrom(src => src.GenderFemale))
                .ForMember(dest => dest.Gender_Male, opt => opt.MapFrom(src => src.GenderMale))
                .ForMember(dest => dest.Gender_Total, opt => opt.MapFrom(src => src.GenderTotal))
                .ForMember(dest => dest.Age_Under18, opt => opt.MapFrom(src => src.AgeUnder18))
                .ForMember(dest => dest.Age_Over18, opt => opt.MapFrom(src => src.AgeOver18))
                .ForMember(dest => dest.Age_Total, opt => opt.MapFrom(src => src.AgeTotal))
                .ForMember(dest => dest.Safty_Index, opt => opt.MapFrom(src => src.SafetyIndex))
                .ForMember(dest => dest.Safty_Percentage, opt => opt.MapFrom(src => src.Safty_Percentage));
        }
    }
}
