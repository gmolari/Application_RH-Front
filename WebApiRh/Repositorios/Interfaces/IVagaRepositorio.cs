using WebApiRh.Models;

namespace WebApiRh.Repositorios.Interfaces
{
    public interface IVagaRepositorio
    {
        Task<List<VagaModel>> BuscarTodasVagas();
        Task<VagaModel> BuscarPorId(int id);
        Task<VagaModel> Adicionar(VagaModel vaga);
        Task<VagaModel> Atualizar(VagaModel vaga, int id);
        Task<bool> Apagar(int id);
    }
}
