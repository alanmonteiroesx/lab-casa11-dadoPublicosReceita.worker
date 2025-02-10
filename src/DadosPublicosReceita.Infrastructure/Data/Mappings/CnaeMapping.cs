using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class CnaeMapping : IEntityTypeConfiguration<Cnae>
    {
        public void Configure(EntityTypeBuilder<Cnae> builder)
        {
            builder.ToTable("Cnae");

            builder.HasKey(x => x.Codigo);

            builder.Property(x=>x.Codigo)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(7);

            builder.Property(x => x.Descricao)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);
        }
    }
}
