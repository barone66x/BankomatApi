using AutoMapper;
using BankomatApi.Dto;
using BankomatSimulator;

namespace BankomatApi.Profiles
{
    public class FunzionalitaProfile : Profile
    {
        public FunzionalitaProfile()
        {
            CreateMap<Funzionalita, FunzionalitaDto>()
                .ReverseMap();


        }
    }
}
