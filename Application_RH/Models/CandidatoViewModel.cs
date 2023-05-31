namespace Application_RH.Models
{
    public class CandidatoViewModel
    {
        public CandidatoViewModel()
        {
            Vagas = new List<VagaModel>();
            Tecnologias = new List<TecnologiaModel>();
        }
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Contato { get; set; }
        public string NomeVaga { get; set; }
        public string Vaga { get; set; }
        public List<VagaModel>? Vagas { get; set; }
        public List<TecnologiaModel>? Tecnologias { get; set; }
        public List<string> StringTecnologias { get; set; }
    }
}
