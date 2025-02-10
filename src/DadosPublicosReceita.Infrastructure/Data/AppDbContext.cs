using DadosPublicosReceita.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DadosPublicosReceita.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Cnae> Cnaes { get; set; } = null!;
        public DbSet<Empresa> Empresas { get; set; } = null!;
        public DbSet<Endereco> Endereco { get; set; } = null!;
        public DbSet<Estabelecimento> Estabelecimentos { get; set; } = null!;
        public DbSet<Municipio> Municipios { get; set; } = null!;
        public DbSet<Pais> Paizes { get; set; } = null!;
        public DbSet<Telefone> Telefones { get; set; } = null!;
        public DbSet<ImportacaoControle> ImportacoesControle { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
