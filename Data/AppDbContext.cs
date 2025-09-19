using Microsoft.EntityFrameworkCore;
using PetTravelInsurance.Models;

namespace PetTravelInsurance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PlanoPet> Planos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Nome)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(t => t.Email)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(t => t.Telefone)
                    .HasMaxLength(20);
                entity.HasIndex(t => t.Email).IsUnique();
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Raca)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Idade)
                    .HasMaxLength(50);
                entity.HasOne(p => p.Tutor)
                    .WithMany(t => t.Pets)
                    .HasForeignKey(p => p.TutorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PlanoPet>(entity =>
            {
                entity.HasKey(pp => pp.Id);
                entity.Property(pp => pp.Nome)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(pp => pp.Preco)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                entity.Property(pp => pp.Cobertura)
                    .HasMaxLength(1000);
                entity.Property(pp => pp.Descricao)
                    .HasMaxLength(500);
                entity.Property(pp => pp.Ativo)
                    .HasDefaultValue(true);
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.DataInicio)
                    .IsRequired();
                entity.Property(c => c.DataFim)
                    .IsRequired();
                entity.Property(c => c.PlanoNome)
                    .HasMaxLength(200);
                entity.Property(c => c.PlanoPreco)
                    .HasColumnType("decimal(18,2)");
                entity.Property(c => c.PlanoCobertura)
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Tutor)
                .WithMany(t => t.Contratos)
                .HasForeignKey(c => c.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Pet)
                .WithMany(p => p.Contratos)
                .HasForeignKey(c => c.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.PlanoPet)
                .WithMany(pp => pp.Contratos)
                .HasForeignKey(c => c.PlanoPetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contrato>()
                .HasIndex(c => c.TutorId)
                .HasDatabaseName("IX_Contratos_TutorId");

            modelBuilder.Entity<Contrato>()
                .HasIndex(c => c.PetId)
                .HasDatabaseName("IX_Contratos_PetId");

            modelBuilder.Entity<Contrato>()
                .HasIndex(c => c.DataInicio)
                .HasDatabaseName("IX_Contratos_DataInicio");

            modelBuilder.Entity<Pet>()
                .HasIndex(p => p.TutorId)
                .HasDatabaseName("IX_Pets_TutorId");
        }
    }
}
