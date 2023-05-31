using Microsoft.EntityFrameworkCore;
using WebApiRh.Data;
using WebApiRh.Models;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh.Repositorios
{
    public class TecnologiaRepositorio : ITecnologiaRepositorio
    {
        private readonly WebApiRhDBContext _dbContext;
        public TecnologiaRepositorio(WebApiRhDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TecnologiaModel> BuscarPorId(int id)
        {
            return await _dbContext.Tecnologias.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TecnologiaModel>> BuscarTodasTecnologias()
        {
            return await _dbContext.Tecnologias.ToListAsync();
        }

        public async Task<TecnologiaModel> Adicionar(TecnologiaModel tecnologia)
        {
            await _dbContext.Tecnologias.AddAsync(tecnologia);
            await _dbContext.SaveChangesAsync();
            return tecnologia;
        }

        public async Task<TecnologiaModel> Atualizar(TecnologiaModel tecnologia, int id)
        {
            TecnologiaModel tecnologiaPorId = await BuscarPorId(id);
            if (tecnologiaPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            if (tecnologia.Nome != null) tecnologiaPorId.Nome = tecnologia.Nome;

            _dbContext.Tecnologias.Update(tecnologiaPorId);
            await _dbContext.SaveChangesAsync();

            return tecnologiaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            TecnologiaModel tecnologiaPorId = await BuscarPorId(id);
            if (tecnologiaPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Tecnologias.Remove(tecnologiaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
