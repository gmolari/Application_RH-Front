using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiRh.Models;
using WebApiRh.Repositorios;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagaController : ControllerBase
    {
        private readonly IVagaRepositorio _vagaRepositorio;

        public VagaController(IVagaRepositorio vagaRepositorio)
        {
            _vagaRepositorio = vagaRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<VagaModel>>> BuscarTodasVagas()
        {
            List<VagaModel> vagas = await _vagaRepositorio.BuscarTodasVagas();
            return Ok(vagas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<VagaModel>>> BuscarPorId(int id)
        {
            VagaModel vaga = await _vagaRepositorio.BuscarPorId(id);
            return Ok(vaga);
        }

        [HttpPost]
        public async Task<ActionResult<VagaModel>> Cadastrar([FromBody] VagaModel vaga)
        {
            VagaModel cVaga = await _vagaRepositorio.Adicionar(vaga);
            return Ok(cVaga);
        }

        [HttpPut("atualizarVaga/{id}")]
        public async Task<ActionResult<List<VagaModel>>> Atualizar(VagaModel newVaga, int id)
        {
            VagaModel vaga = await _vagaRepositorio.Atualizar(newVaga, id);
            return Ok(vaga);
        }
        [HttpDelete("deletarVaga/{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _vagaRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}
