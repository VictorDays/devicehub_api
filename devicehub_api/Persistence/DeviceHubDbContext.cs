using devicehub_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace devicehub_api.Persistence
{
    public class DeviceHubDbContext : DbContext
    {
        public DeviceHubDbContext(DbContextOptions<DeviceHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Garantia> Garantias { get; set; }
        public DbSet<Licenca> Licencas { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações de relacionamento
            modelBuilder.Entity<Ativo>()
                .HasOne(a => a.Departamento)
                .WithMany(d => d.Ativos)
                .HasForeignKey(a => a.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ativo>()
                .HasOne(a => a.Fornecedor)
                .WithMany(f => f.Ativos)
                .HasForeignKey(a => a.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ativo>()
                .HasOne(a => a.Responsavel)
                .WithMany(f => f.Ativos)
                .HasForeignKey(a => a.ResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Garantia>()
                .HasOne(g => g.Ativo)
                .WithOne(a => a.Garantia)
                .HasForeignKey<Garantia>(g => g.AtivoId);

            modelBuilder.Entity<Garantia>()
                .HasOne(g => g.Fornecedor)
                .WithMany()
                .HasForeignKey(g => g.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Licenca>()
                .HasOne(l => l.Ativo)
                .WithMany(a => a.Licencas)
                .HasForeignKey(l => l.AtivoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.Ativo)
                .WithMany(a => a.Manutencoes)
                .HasForeignKey(m => m.AtivoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Departamento)
                .WithMany(d => d.Funcionarios)
                .HasForeignKey(f => f.DepartamentoId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
