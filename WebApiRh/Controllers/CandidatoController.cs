using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiRh.Models;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly ICandidatoRepositorio _candidatoRepositorio;

        public CandidatoController(ICandidatoRepositorio candidatoRepositorio)
        {
            _candidatoRepositorio = candidatoRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<CandidatoModel>>> BuscarTodosCandidatos()
        {
            List<CandidatoModel> candidatos = await _candidatoRepositorio.BuscarTodosCandidatos();
            return Ok(candidatos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CandidatoModel>>> BuscarPorId(int id)
        {
            CandidatoModel candidato = await _candidatoRepositorio.BuscarPorId(id);
            return Ok(candidato);
        }

        [HttpPost]
        public async Task<ActionResult<CandidatoModel>> Cadastrar([FromBody] CandidatoModel candidato)
        {
            CandidatoModel cCandidato = await _candidatoRepositorio.Adicionar(candidato);
            return Ok(cCandidato);
        }

        [HttpPut("atualizarCandidato/{id}")]
        public async Task<ActionResult<List<CandidatoModel>>> Atualizar(CandidatoModel newCandidato, int id)
        {
            CandidatoModel candidato = await _candidatoRepositorio.Atualizar(newCandidato, id);
            return Ok(candidato);
        }
        [HttpDelete("deletarCandidato/{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _candidatoRepositorio.Apagar(id);
            return Ok(apagado);
        }
        
    }
}
