using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiRh.Models;

namespace WebApiRh.Data.Map
{
    public class CandidatoMap : IEntityTypeConfiguration<CandidatoModel>
    {
        public void Configure(EntityTypeBuilder<CandidatoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Contato).IsRequired().HasMaxLength(64);
        }
    }
}
