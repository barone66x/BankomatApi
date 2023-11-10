namespace BankomatApi.Dto
{
    public class ContiCorrenteDto
    {
        public long Id { get; set; }
        public long IdUtente { get; set; }
        public int Saldo { get; set; }
        public System.DateTime DataUltimaOperazione { get; set; }
    }
}
