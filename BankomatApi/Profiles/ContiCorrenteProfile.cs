using AutoMapper;
using BankomatApi.Dto;
using BankomatSimulator;

namespace BankomatApi.Profiles
{
    public class ContiCorrenteProfile : Profile
    {
        public ContiCorrenteProfile()
        {
            CreateMap<ContiCorrente, ContiCorrenteDto>()
                .ReverseMap();


        }
    }
}
