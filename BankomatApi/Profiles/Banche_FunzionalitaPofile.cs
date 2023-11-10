using AutoMapper;
using BankomatApi.Dto;
using BankomatSimulator;

namespace BankomatApi.Profiles
{
    public class Banche_Funzionalita : Profile
    {
        public Banche_Funzionalita()
        {
            CreateMap<Banche_Funzionalita, Banche_FunzionalitaDto>()
                .ReverseMap();
        }
    }
}
