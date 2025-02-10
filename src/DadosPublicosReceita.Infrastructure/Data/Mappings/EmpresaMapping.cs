using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresa");

            builder.HasKey(x => x.CnpjBasico);

            builder.HasIndex(x => x.RazaoSocial, "IX_Empresa_RazaoSocial");

            builder.Property(x => x.CnpjBasico)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(8);

            builder.Property(x => x.RazaoSocial)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);

            builder.Property(x => x.NaturezaJuridica)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(4);

            builder.Property(x => x.QualificacaoResponsavel)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(2);

            builder.Property(x => x.CapitalSocial)
                .IsRequired(true)
                .HasColumnType("MONEY")
                .HasPrecision(18,4);

            builder.Property(x => x.EnteFederativoResp)
                .IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.PorteEmpresa)
                .IsRequired(true)
                .HasColumnType("INT");
        }
    }
}
