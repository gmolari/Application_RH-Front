using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiRh.Models;

namespace WebApiRh.Data.Map
{
    public class VagaMap : IEntityTypeConfiguration<VagaModel>
    {
        public void Configure(EntityTypeBuilder<VagaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(64);
        }
    }
}
