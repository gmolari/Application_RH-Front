using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiRh.Models;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnologiaController : ControllerBase
    {
        private readonly ITecnologiaRepositorio _tecnologiaRepositorio;

        public TecnologiaController(ITecnologiaRepositorio tecnologiaRepositorio)
        {
            _tecnologiaRepositorio = tecnologiaRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<TecnologiaModel>>> BuscarTodasTecnologias()
        {
            List<TecnologiaModel> tecnologia = await _tecnologiaRepositorio.BuscarTodasTecnologias();
            return Ok(tecnologia);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TecnologiaModel>>> BuscarPorId(int id)
        {
            TecnologiaModel tecnologia = await _tecnologiaRepositorio.BuscarPorId(id);
            return Ok(tecnologia);
        }

        [HttpPost]
        public async Task<ActionResult<TecnologiaModel>> Cadastrar([FromBody] TecnologiaModel tecnologia)
        {
            TecnologiaModel cTecnologia = await _tecnologiaRepositorio.Adicionar(tecnologia);
            return Ok(cTecnologia);
        }

        [HttpPut("atualizarTecnologia/{id}")]
        public async Task<ActionResult<List<TecnologiaModel>>> Atualizar(TecnologiaModel newTecnologia, int id)
        {
            TecnologiaModel tecnologia = await _tecnologiaRepositorio.Atualizar(newTecnologia, id);
            return Ok(tecnologia);
        }
        [HttpDelete("deletarTecnologia/{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _tecnologiaRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}
