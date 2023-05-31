using Microsoft.EntityFrameworkCore;
using WebApiRh.Data;
using WebApiRh.Models;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh.Repositorios
{
    public class VagaRepositorio : IVagaRepositorio
    {
        private readonly WebApiRhDBContext _dbContext;
        public VagaRepositorio(WebApiRhDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<VagaModel> BuscarPorId(int id)
        {
            return await _dbContext.Vagas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<VagaModel>> BuscarTodasVagas()
        {
            return await _dbContext.Vagas.ToListAsync();
        }

        public async Task<VagaModel> Adicionar(VagaModel vaga)
        {
            await _dbContext.Vagas.AddAsync(vaga);
            await _dbContext.SaveChangesAsync();
            return vaga;
        }

        public async Task<VagaModel> Atualizar(VagaModel vaga, int id)
        {
            VagaModel vagaPorId = await BuscarPorId(id);
            if (vagaPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            if (vaga.Nome != null) vagaPorId.Nome = vaga.Nome;

            _dbContext.Vagas.Update(vagaPorId);
            await _dbContext.SaveChangesAsync();

            return vagaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            VagaModel vagaPorId = await BuscarPorId(id);
            if (vagaPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Vagas.Remove(vagaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
