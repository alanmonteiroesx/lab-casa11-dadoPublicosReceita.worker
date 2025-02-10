using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class PaisMapping : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais");

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(3);

            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);
        }
    }
}
