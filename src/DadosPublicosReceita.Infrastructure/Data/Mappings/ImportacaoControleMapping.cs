using DadosPublicosReceita.Domain.Entities;
using DadosPublicosReceita.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class ImportacaoControleMapping : IEntityTypeConfiguration<ImportacaoControle>
    {
        public void Configure(EntityTypeBuilder<ImportacaoControle> builder)
        {
            builder.ToTable("ImportacaoControle");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AnoMes)
                .HasColumnName("AnoMes")
                .HasColumnType("varchar")
                .HasMaxLength(7)
                .IsRequired();

            builder.Property(x => x.NomeArquivo)
                .HasColumnName("NomeArquivo")
                .HasColumnType("varchar(100)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<EStatusImportacaoType>(v)
                )
                .IsRequired();

            builder.Property(x => x.DataInicio)
                .HasColumnName("DataInicio")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.DataConclusao)
                .HasColumnName("DataConclusao")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.QuantidadeRegistros)
                .HasColumnName("QuantidadeRegistros")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.UltimoRegistroProcessado)
                .HasColumnName("UltimoRegistroProcessado")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Erro)
                .HasColumnName("Erro")
                .HasColumnType("varchar(max)")
                .IsRequired(false);

            builder.Property(x => x.VersaoArquivo)
                .HasColumnName("VersaoArquivo")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}