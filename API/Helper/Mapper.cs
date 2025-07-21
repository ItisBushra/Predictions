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
            CreateMap<SafetyTrends, SafetyTrendsDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Demographic, opt => opt.MapFrom(src => src.Demographic));

        }
    }
}
