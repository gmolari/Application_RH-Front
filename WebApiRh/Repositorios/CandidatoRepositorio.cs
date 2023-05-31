using Microsoft.EntityFrameworkCore;
using WebApiRh.Data;
using WebApiRh.Models;
using WebApiRh.Repositorios.Interfaces;

namespace WebApiRh.Repositorios
{
    public class CandidatoRepositorio : ICandidatoRepositorio
    {
        private readonly WebApiRhDBContext _dbContext;
        public CandidatoRepositorio(WebApiRhDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CandidatoModel> BuscarPorId(int id)
        {
            return await _dbContext.Candidatos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CandidatoModel>> BuscarTodosCandidatos()
        {
            return await _dbContext.Candidatos.ToListAsync();
        }

        public async Task<CandidatoModel> Adicionar(CandidatoModel candidato)
        {
            await _dbContext.Candidatos.AddAsync(candidato);
            await _dbContext.SaveChangesAsync();
            return candidato;
        }

        public async Task<CandidatoModel> Atualizar(CandidatoModel candidato, int id)
        {
            CandidatoModel candidatoPorId = await BuscarPorId(id);
            if (candidatoPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            if (candidatoPorId.Nome != null) candidatoPorId.Nome = candidato.Nome;

            _dbContext.Candidatos.Update(candidatoPorId);
            await _dbContext.SaveChangesAsync();

            return candidatoPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            CandidatoModel candidatoPorId = await BuscarPorId(id);
            if (candidatoPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Candidatos.Remove(candidatoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
