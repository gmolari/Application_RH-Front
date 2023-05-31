using WebApiRh.Models;

namespace WebApiRh.Repositorios.Interfaces
{
    public interface ICandidatoRepositorio 
    {
        Task<List<CandidatoModel>> BuscarTodosCandidatos();
        Task<CandidatoModel> BuscarPorId(int id);
        Task<CandidatoModel> Adicionar(CandidatoModel candidato);
        Task<CandidatoModel> Atualizar(CandidatoModel candidato, int id);
        Task<bool> Apagar(int id);
    }
}
