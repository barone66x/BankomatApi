using AutoMapper;
using BankomatApi.Dto;
using BankomatApi.Repositories;
using BankomatSimulator;
using Microsoft.AspNetCore.Mvc;

namespace BankomatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancheController : ControllerBase
    {
        IDbRepository _dbRepo;
        private readonly IMapper _mapper;
        public BancheController(IDbRepository db, IMapper map)
        {
            _dbRepo = db;
            _mapper = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BancheDto>>> GetBancheAsync()
        {
            var banche = await _dbRepo.GetBancheAsync();
            // var productsDto = _mapper.Map<IEnumerable<ProductDto>>(snacks);
            return Ok(_mapper.Map<IEnumerable<Banche>, IEnumerable<BancheDto>>(banche));


        }



        [HttpGet("{bancaId}", Name = "GetBancaById")]
        public async Task<ActionResult<UtentiDto>> GetBancaById(long bancaId)
        {
            var banca = await _dbRepo.GetBancaAsync(bancaId);

            if (banca == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Banche, BancheDto>(banca));
        }
    }
}
