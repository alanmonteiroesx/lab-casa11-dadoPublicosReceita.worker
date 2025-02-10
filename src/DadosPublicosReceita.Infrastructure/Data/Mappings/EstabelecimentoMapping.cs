using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class EstabelecimentoMapping : IEntityTypeConfiguration<Estabelecimento>
    {
        public void Configure(EntityTypeBuilder<Estabelecimento> builder)
        {
            builder.ToTable("Estabelecimento");

            builder.HasKey(x => new { x.CnpjBasico, x.CnpjOrdem, x.CnpjDv })
                .HasName("PK_Estabelecimento");

            builder.Property(x => x.CnpjBasico)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(8);

            builder.Property(x => x.CnpjOrdem)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(4);

            builder.Property(x => x.CnpjDv)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(2);

            builder.Property(x => x.TipoDoEstabelecimento)
                .IsRequired(true)
                .HasColumnType("TINYINT");

            builder.Property(x => x.NomeFantasia)
                .IsRequired(false)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.DataInicioAtividade)
                .IsRequired(true)
                .HasColumnType("DATE");

            builder.Property(x => x.SituacaoCadastral)
                .IsRequired(true)
                .HasColumnType("TINYINT");

            builder.Property(x => x.DataSituacaoCadastral)
                .IsRequired(true)
                .HasColumnType("DATE");

            builder.Property(x => x.MotivoSituacaoCadastral)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(4);

            builder.Property(x => x.SituacaoEspecial)
                .IsRequired(false)
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.DataSituacaoEspecial)
                .IsRequired(false)
                .HasColumnType("DATE");

            builder.Property(x => x.Email)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.CnaePrincipalId)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(7);

            builder.HasOne(e => e.CnaePrincipal)
                .WithMany()
                .HasForeignKey(e => e.CnaePrincipalId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
