using BankomatApi.Dto;
using BankomatSimulator;

namespace BankomatApi.Repositories
{
    public interface IDbRepository
    {
        Task<IEnumerable<Utenti>> GetUtentiAsync();
        Task<Utenti> GetUtenteAsync(long id);
        Task<bool> RemoveUtenteAsync(long id);
        Task<Utenti> AddUtenteAsync(UtentiDtoToAdd utente);
        Task<bool> UpdateUtenteAsync(UtentiDtoToUpdate utente, long id);


        Task<IEnumerable<Banche>> GetBancheAsync();
        Task<Banche> GetBancaAsync(long id);


        Task<IEnumerable<Funzionalita>> GetFunzionalitasAsync();
        Task<Funzionalita> GetFunzionalitaAsync(long id);


        Task<Banche_Funzionalita> GetFunzionalitaBancaAsync(int idBanca , int idFunzionalita);
        Task<bool> RemoveFunzionalitaBancaAsync(Banche_Funzionalita banche_Funzionalita);

        Task<bool> AddFunzionalitaBancaAsync(int idBanca, int idFunzionalita);
        Task<IEnumerable<Funzionalita>> GetFunzionalitasBancaAsync(int idBanca);


    }
}
