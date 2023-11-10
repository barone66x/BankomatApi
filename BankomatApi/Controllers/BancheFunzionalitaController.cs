using AutoMapper;
using BankomatApi.Dto;
using BankomatApi.Repositories;
using BankomatSimulator;
using Microsoft.AspNetCore.Mvc;

namespace BankomatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancheFunzionalitaController : ControllerBase
    {
        IDbRepository _dbRepo;
        private readonly IMapper _mapper;
        public BancheFunzionalitaController(IDbRepository db, IMapper map)
        {
            _dbRepo = db;
            _mapper = map;
        }


        //[HttpGet("{bancaId}/{funzionalitaId}", Name = "GetFunzionalitaBancaById")]
        //public async Task<ActionResult<Banche_Funzionalita>> GetFunzionalitaBancaByIdAsync(int bancaId, int funzionalitaId)
        //{
        //    var banche_Funzionalita = await _dbRepo.GetFunzionalitaBancaAsync(bancaId, funzionalitaId);
        //    if (banche_Funzionalita == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<Banche_Funzionalita, Banche_FunzionalitaDto>(banche_Funzionalita));
        //}

        [HttpGet("{bancaId}", Name = "GetFunzionalitasBancaById")]
        public async Task<ActionResult<IEnumerable<Banche_Funzionalita>>> GetFunzionalitasBancaByIdAsync(int bancaId)
        {
            
            var funzionalitas = await _dbRepo.GetFunzionalitasBancaAsync(bancaId);
            if (funzionalitas == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<Funzionalita>, IEnumerable<FunzionalitaDto>>(funzionalitas));
        }

        [HttpDelete("{bancaId}/{funzionalitaId}")]
        public async Task<ActionResult<Banche_Funzionalita>> RemoveFunzionalitaBancaAsync(int bancaId, int funzionalitaId)
        {
            var funzionalitaBancaToRemove = await _dbRepo.GetFunzionalitaBancaAsync(bancaId, funzionalitaId);
            if (funzionalitaBancaToRemove == null)
            {
                return NotFound();
            }

            var result = await _dbRepo.RemoveFunzionalitaBancaAsync(funzionalitaBancaToRemove);
            return NoContent();
        }


        [HttpPost("{bancaId}/{funzionalitaId}")]
        public async Task<ActionResult<Banche_Funzionalita>> AddFunzionalitaBancaAsync(int bancaId, int funzionalitaId )
        {

            var banca = await _dbRepo.GetBancaAsync(bancaId);
            if (banca == null)
            {
                return BadRequest("banca non trovata");
            }

            var funzionalita = await _dbRepo.GetFunzionalitaAsync(funzionalitaId);
            if (funzionalita == null)
            {
                return BadRequest("funzionalità non trovata");
            }

            var funzionalitaBanca = await _dbRepo.AddFunzionalitaBancaAsync(bancaId, funzionalitaId);

            return NoContent();
        }
    }
}