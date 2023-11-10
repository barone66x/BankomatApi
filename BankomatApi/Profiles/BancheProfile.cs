using AutoMapper;
using BankomatApi.Dto;
using BankomatSimulator;

namespace BankomatApi.Profiles
{
    public class BancheProfile : Profile
    {
        public BancheProfile()
        {
            CreateMap<Banche, BancheDto>()
                .ReverseMap();


        }
    }
}
