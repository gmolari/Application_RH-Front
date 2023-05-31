using WebApiRh.Models;

namespace WebApiRh.Repositorios.Interfaces
{
    public interface ITecnologiaRepositorio 
    {
        Task<List<TecnologiaModel>> BuscarTodasTecnologias();
        Task<TecnologiaModel> BuscarPorId(int id);
        Task<TecnologiaModel> Adicionar(TecnologiaModel tecnologia);
        Task<TecnologiaModel> Atualizar(TecnologiaModel tecnologia, int id);
        Task<bool> Apagar(int id);
    }
}
