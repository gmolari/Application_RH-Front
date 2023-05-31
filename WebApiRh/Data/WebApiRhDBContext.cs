using Microsoft.EntityFrameworkCore;
using WebApiRh.Data.Map;
using WebApiRh.Models;

namespace WebApiRh.Data
{
    public class WebApiRhDBContext : DbContext
    {
        public WebApiRhDBContext(DbContextOptions<WebApiRhDBContext> options) : base(options)
        {
           
        }

        public DbSet<CandidatoModel> Candidatos { get; set; }
        public DbSet<TecnologiaModel> Tecnologias { get; set; }
        public DbSet<VagaModel> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CandidatoMap());
            modelBuilder.ApplyConfiguration(new TecnologiaMap());
            modelBuilder.ApplyConfiguration(new VagaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
