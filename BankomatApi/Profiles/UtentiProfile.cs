using AutoMapper;
using BankomatApi.Dto;
using BankomatSimulator;

namespace BankomatApi.Profiles
{
    public class UtentiProfile : Profile
    {
        public UtentiProfile()
        {
            CreateMap<Utenti, UtentiDto>()
                .ReverseMap();

            CreateMap<Utenti, UtentiDtoToAdd>()
                .ReverseMap();
            CreateMap<Utenti, UtentiDtoToUpdate>()
                .ReverseMap();
            CreateMap<UtentiDtoToAdd, UtentiDtoToUpdate>()
                .ReverseMap();

        }
    }
}
