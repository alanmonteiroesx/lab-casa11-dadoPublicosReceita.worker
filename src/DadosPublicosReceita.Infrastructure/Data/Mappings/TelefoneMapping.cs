using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("Telefone");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .HasColumnType("VARCHAR")
                .HasMaxLength(5);

            builder.Property(x => x.Numero)
                .HasColumnType("VARCHAR")
                .HasMaxLength(9);

            builder.Property(x => x.EstabelecimentoCnpjBasico)
                .HasColumnType("CHAR")
                .HasMaxLength(8);

            builder.Property(x => x.EstabelecimentoCnpjOrdem)
                .HasColumnType("CHAR")
                .HasMaxLength(4);

            builder.Property(x => x.EstabelecimentoCnpjDv)
                .HasColumnType("CHAR")
                .HasMaxLength(2);

            builder.HasOne(x => x.Estabelecimento)
                .WithMany(e => e.Telefones)
                .HasForeignKey(x => new {
                    x.EstabelecimentoCnpjBasico,
                    x.EstabelecimentoCnpjOrdem,
                    x.EstabelecimentoCnpjDv
                })
                .HasConstraintName("FK_Telefone_Estabelecimento")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new {
                x.EstabelecimentoCnpjBasico,
                x.EstabelecimentoCnpjOrdem,
                x.EstabelecimentoCnpjDv
            }).HasDatabaseName("IX_Telefone_Estabelecimento");
        }
    }
}
