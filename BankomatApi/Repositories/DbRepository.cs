using AutoMapper;
using BankomatApi.Dto;
using BankomatSimulator;
using Microsoft.EntityFrameworkCore;

namespace BankomatApi.Repositories
{
    public class DbRepository : IDbRepository
    {

        private EsercitazioneBancaEntities _ctx;
        private readonly IMapper _mapper;
        public DbRepository(
            EsercitazioneBancaEntities ctx
            ,IMapper mapper
            )
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _mapper = mapper;
        }


        public async Task<bool> SaveChanges() //metodo chiamato per salvare le modifiche su database in modo asincrono
        {
            return (await _ctx.SaveChangesAsync() >= 0);
        }


        #region Utente
        //metodi per la gestione degli utenti
        public async Task<Utenti> AddUtenteAsync(UtentiDtoToAdd utente)
        {

            //utenteToAdd.Id = await _ctx.Utenti.MaxAsync(dr => dr.Id) + 1;
            Utenti utenteToAdd = _mapper.Map<Utenti>(utente);

            ContiCorrente newConto = new ContiCorrente();
                      
            newConto.Saldo = 0;
            newConto.DataUltimaOperazione = DateTime.Today;
           

            utenteToAdd.ContiCorrente.Add(newConto);
            utenteToAdd.Bloccato = false;
            utenteToAdd.IdBanca = utente.IdBanca;


            //await _ctx.ContiCorrente.AddAsync(newConto);
            await _ctx.Utenti.AddAsync(utenteToAdd);
            await SaveChanges();
            return utenteToAdd;
        }

        public async Task<Utenti> GetUtenteAsync(long id)
        {
            return await _ctx.Utenti.Include(c => c.ContiCorrente).Include(b => b.Banche).FirstOrDefaultAsync(dr => dr.Id == id); //lambda che ottiene uno specifico utente.
            //.Include() viene inserito per ottenere un cliente contenente i riferimenti ai suoi conti correnti e alla sua banca
        }

        public async Task<IEnumerable<Utenti>> GetUtentiAsync()
        {
            return await _ctx.Utenti.Include(c => c.ContiCorrente).Include(b => b.Banche).OrderBy(c => c.NomeUtente).ToListAsync(); //lambda che ottiene tutti gli utenti di una specifica 
        }

        public async Task<bool> RemoveUtenteAsync(long id)
        {
            bool UtenteRimuovibile = true;
            var utente = await GetUtenteAsync(id); //ottengo l'utente che voglio rimuovere dal database
            var contiUtente =  utente.ContiCorrente.ToList();
           
            if (contiUtente != null )
            {
                
                foreach (var conto in contiUtente)
                {
                    if (conto.Saldo > 0) //Controllo se l'utente ha almeno un conto con saldo superiore a 0
                    {
                        UtenteRimuovibile = false; //se lo ha non posso rimuoverlo
                    }                  
                }
                if (UtenteRimuovibile)
                {
                    foreach (var conto in contiUtente)
                    {
                        _ctx.ContiCorrente.Remove(conto); //se posso rimuovere l'utente, prima rimuovo tutti i suoi conti
                        
                    }
                    _ctx.Utenti.Remove(utente); //infine rimuovo l'utente
                    return await SaveChanges();

                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                _ctx.Utenti.Remove(utente);
                return await SaveChanges();
            }
            

        }

        public async Task<bool> UpdateUtenteAsync(UtentiDtoToUpdate utente, long id)
        {
            var internalUtente = await _ctx.Utenti.FirstOrDefaultAsync(p => p.Id == id); //ottengo l'utente, 
            internalUtente.NomeUtente = utente.NomeUtente; //modifico i campi dell'utente del database con i campi dell'utente che passo
            internalUtente.Password = utente.Password;
            internalUtente.Bloccato = utente.Bloccato;
            return await SaveChanges();
        }
        #endregion Utente


        #region Banche
        public async Task<Banche> GetBancaAsync(long id) //il codice delle banche è molto simile a quello degli utenti
        {
            return await _ctx.Banche.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Banche>> GetBancheAsync()
        {
            return await _ctx.Banche.ToListAsync();
        }
        #endregion

        #region Funzionalita

        public async Task<IEnumerable<Funzionalita>> GetFunzionalitasAsync() //ottengo tutte le funzionalità
        {
            return await _ctx.Funzionalita.ToListAsync();
        }

        public async Task<Funzionalita> GetFunzionalitaAsync(long id) //ottengo una specifica funzionalità
        {
            return await _ctx.Funzionalita.FirstOrDefaultAsync(f => f.Id == id);
        }

        #endregion

        #region BancheFunzionalita

        public async Task<Banche_Funzionalita> GetFunzionalitaBancaAsync(int idBanca, int idFunzionalita) //ottengo le funzionalità di una specifica banca
        {
            return await _ctx.Banche_Funzionalita.FirstOrDefaultAsync(f => f.IdFunzionalita == idFunzionalita && f.IdBanca == idBanca );
            //lambda con 2 condizioni
        }
        public async Task<bool> RemoveFunzionalitaBancaAsync(Banche_Funzionalita banche_Funzionalita)
        {
            
            _ctx.Banche_Funzionalita.Remove(banche_Funzionalita);

            return await SaveChanges();
        }

        public async Task<bool> AddFunzionalitaBancaAsync(int idBanca, int idFunzionalita)
        {
            Banche_Funzionalita banche_Funzionalita = new Banche_Funzionalita();
            var banca = await GetBancaAsync(idBanca); 
            banche_Funzionalita.Banche = banca; //Setto il campo banca della mia Banche_Funzionalità
            var newFunzionalita = await GetFunzionalitaAsync(idFunzionalita);
            banche_Funzionalita.Funzionalita = newFunzionalita;
            await _ctx.Banche_Funzionalita.AddAsync(banche_Funzionalita);
            await _ctx.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Funzionalita>> GetFunzionalitasBancaAsync(int idBanca)
        {
            var banca = await GetBancaAsync(idBanca);
            if (banca == null)
            {
                return null;
            }
            return await _ctx.Funzionalita.Where(x => x.Banche_Funzionalita.Where(y => y.IdBanca == idBanca).Count()>0 ).ToListAsync();
        }

        #endregion
    }
}
