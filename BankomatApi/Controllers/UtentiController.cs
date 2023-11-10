using AutoMapper;
using BankomatApi.Dto;
using BankomatApi.Repositories;
using BankomatSimulator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankomatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiController : ControllerBase
    {
        IDbRepository _dbRepo;
        private readonly IMapper _mapper;
        public UtentiController(IDbRepository db, IMapper map)
        {
            _dbRepo = db;
            _mapper = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtentiDto>>> GetUtentiAsync()
        {
            var utenti = await _dbRepo.GetUtentiAsync();
            // var productsDto = _mapper.Map<IEnumerable<ProductDto>>(snacks);
            return Ok(_mapper.Map<IEnumerable<Utenti>, IEnumerable<UtentiDto>>(utenti));
        }

     

        [HttpGet("{utenteId}", Name = "GetUtenteById")]
        public async Task<ActionResult<UtentiDto>> GetUtenteById(long utenteId)
        {
            var utente = await _dbRepo.GetUtenteAsync(utenteId);
            if (utente == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Utenti,UtentiDto>(utente));         
        }


        [HttpDelete("{utenteId}")]
        public async Task<ActionResult<Utenti>> RemoveUtente(long utenteId)
        {
            var utenteToRemove = await _dbRepo.GetUtenteAsync(utenteId);
            if (utenteToRemove == null)
            {
                return NotFound();
            }

            var result = await _dbRepo.RemoveUtenteAsync(utenteToRemove.Id);
            if (result == true)
            {
                return NoContent();
            }
            return BadRequest("utente non cancellabile");
            
        }

        [HttpPut("{utenteId}")]
        public async Task<ActionResult<Utenti>> UpdateUtente(int utenteId, [FromBody] UtentiDtoToUpdate utente)
        {
            var utenteToUpdate = await _dbRepo.GetUtenteAsync(utenteId);


            if (utenteToUpdate == null)
            {
                return NotFound();
            }
            

            if (!ModelState.IsValid)
            {
                return BadRequest("modello non valido");
            }
            //utenteToUpdate.NomeUtente = utente.NomeUtente;
            //utenteToUpdate.Password = utente.Password;
            //utenteToUpdate.Bloccato = utente.Bloccato;
            
            var result = await _dbRepo.UpdateUtenteAsync(utente, utenteId);

            return CreatedAtRoute(nameof(GetUtenteById),
               routeValues: new
               {
                   utenteId = utenteToUpdate.Id
               },
               utente);
        }

        [HttpPost]
        public async Task<ActionResult<Utenti>> AddUtente([FromBody] UtentiDtoToAdd utente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("modello non valido");
            }

            var banca = await _dbRepo.GetBancaAsync(utente.IdBanca);
            if (banca == null)
            {
                return BadRequest("banca non trovata");
            }

            var utenteToReturn = await _dbRepo.AddUtenteAsync(utente);

            return CreatedAtRoute(nameof(GetUtenteById),
               routeValues: new
               {
                   utenteId = utenteToReturn.Id
               },
               utente);

        }
    }
}
