using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DadosPublicosReceita.Infrastructure.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TipoLogradouro)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(40);

            builder.Property(x => x.Logradouro)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Numero)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.Complemento)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250);

            builder.Property(x => x.Bairro)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Cep)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(8);

            builder.Property(x => x.Uf)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(2);

            builder.Property(x => x.MunicipioId)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(4);

            builder.Property(x => x.PaisId)
                .IsRequired(true)
                .HasColumnType("CHAR")
                .HasMaxLength(3);

            builder.HasOne(x => x.Municipio)
                .WithMany()
                .HasForeignKey(x => x.MunicipioId)
                .HasConstraintName("FK_Endereco_Municipio")
                .OnDelete(DeleteBehavior.Restrict);

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
                .WithOne(e => e.Endereco)
                .HasForeignKey<Endereco>(x => new {
                    x.EstabelecimentoCnpjBasico,
                    x.EstabelecimentoCnpjOrdem,
                    x.EstabelecimentoCnpjDv
                })
                .HasConstraintName("FK_Endereco_Estabelecimento")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Pais)
                .WithMany()
                .HasForeignKey(x => x.PaisId)
                .HasConstraintName("FK_Endereco_Pais")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
