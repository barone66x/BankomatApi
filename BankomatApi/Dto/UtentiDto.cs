using BankomatSimulator;

namespace BankomatApi.Dto
{
    public class UtentiDto
    {
        public long Id { get; set; }
        public long IdBanca { get; set; }
        public string NomeUtente { get; set; }
        public string Password { get; set; }
        public bool Bloccato { get; set; }

        public  BancheDto Banche { get; set; }
        public ICollection<ContiCorrenteDto> ContiCorrente { get; set; }
    }
}
