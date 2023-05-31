namespace Application_RH.Models
{
    public class TecnologiaViewModel
    {
        public TecnologiaViewModel()
        {
            Vagas = new List<VagaModel>();
        }
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<VagaModel>? Vagas { get; set; }
        public string[] pesoTecs { get; set; }

    }
}
