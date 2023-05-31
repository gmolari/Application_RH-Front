using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiRh.Models;

namespace WebApiRh.Data.Map
{
    public class TecnologiaMap : IEntityTypeConfiguration<TecnologiaModel>
    {
        public void Configure(EntityTypeBuilder<TecnologiaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(64);
        }
    }
}
