using AutoMapper;
using BankomatApi.Dto;
using BankomatApi.Repositories;
using BankomatSimulator;
using Microsoft.AspNetCore.Mvc;

namespace BankomatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunzionalitaController : ControllerBase
    {
        IDbRepository _dbRepo;
        private readonly IMapper _mapper;
        public FunzionalitaController(IDbRepository db, IMapper map)
        {
            _dbRepo = db;
            _mapper = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FunzionalitaDto>>> GetBancheAsync()
        {
            var funzionalitas = await _dbRepo.GetFunzionalitasAsync();
            // var productsDto = _mapper.Map<IEnumerable<ProductDto>>(snacks);
            return Ok(_mapper.Map<IEnumerable<Funzionalita>, IEnumerable<FunzionalitaDto>>(funzionalitas));


        }



        [HttpGet("{funzionalitaId}", Name = "GetFunzionalitaById")]
        public async Task<ActionResult<FunzionalitaDto>> GetFunzionalitaById(long funzionalitaId)
        {
            var funzionalita = await _dbRepo.GetFunzionalitaAsync(funzionalitaId);

            if (funzionalita == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Funzionalita, FunzionalitaDto>(funzionalita));
        }
    }
}
