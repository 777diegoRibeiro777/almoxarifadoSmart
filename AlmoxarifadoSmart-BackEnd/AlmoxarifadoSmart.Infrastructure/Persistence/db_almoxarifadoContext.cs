using System;
using System.Collections.Generic;
using AlmoxarifadoSmart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace AlmoxarifadoSmart.API
{
    public partial class db_almoxarifadoContext : DbContext
    {
     
          
     
        public db_almoxarifadoContext()
        {
        }

        public db_almoxarifadoContext(DbContextOptions<db_almoxarifadoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaMotivo> CategoriaMotivos { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public virtual DbSet<Motivo> Motivos { get; set; } = null!;
        public virtual DbSet<Produto> Produtos { get; set; } = null!;
        public virtual DbSet<Requisicao> Requisicaos { get; set; } = null!;

        public virtual DbSet<LogModel> LOGROBO { get; set; } = null!;

        public virtual DbSet<Benchmarking> Benchmarkings { get; set; } = null!;

        public DbSet<ProdutoScraperModel> ProdutoScraper { get; set; }
        public DbSet<StoreProdutoModel> StoreProdutos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=dbbenchmarking.cfk2cqm644xo.us-east-2.rds.amazonaws.com;Initial Catalog=almoxarifadoequipe04;User Id=admin;Password=master12;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaMotivo>(entity =>
            {
                entity.ToTable("CategoriaMotivo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("Funcionario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(255)
                    .HasColumnName("cargo");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Motivo>(entity =>
            {
                entity.ToTable("Motivo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategoriamotivo).HasColumnName("id_categoriamotivo");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.HasOne(d => d.IdCategoriamotivoNavigation)
                    .WithMany(p => p.Motivos)
                    .HasForeignKey(d => d.IdCategoriamotivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Motivo__id_categ__45F365D3");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .HasColumnName("descricao");

                entity.Property(e => e.EstoqueAtual)
                    .HasColumnName("estoque_atual")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EstoqueMinimo).HasColumnName("estoque_minimo");

                entity.Property(e => e.Preco)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("preco");

                entity.HasMany(d => d.IdRequisicaos)
     .WithMany(p => p.IdProdutos)
     .UsingEntity<Dictionary<string, object>>(
         "ProdutoRequisicao",
         l => l.HasOne<Requisicao>().WithMany().HasForeignKey("IdRequisicao").HasConstraintName("FK__ProdutoRe__id_re__4316F928"),
         r => r.HasOne<Produto>().WithMany().HasForeignKey("IdProduto").HasConstraintName("FK__ProdutoRe__id_pr__4F7CD00D"),
         j =>
         {
             j.HasKey("IdProduto", "IdRequisicao").HasName("PK__ProdutoR__9781356BD7E2EC26");

             j.ToTable("ProdutoRequisicao");

             j.IndexerProperty<int>("IdProduto").HasColumnName("id_produto");

             j.IndexerProperty<int>("IdRequisicao").HasColumnName("id_requisicao");
         });

                entity.HasOne(p => p.Branchmarking)
         .WithOne(b => b.ProdutoNavegation)
         .HasForeignKey<Benchmarking>(p => p.IdProduto)
         .OnDelete(DeleteBehavior.Cascade)
         .HasConstraintName("FK_Produto_Benchmarking");

                entity.HasOne(p => p.ProdutoScraperModel)
                    .WithOne(ps => ps.Produto)
                    .HasForeignKey<ProdutoScraperModel>(ps => ps.IdProduto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Produto_ProdutoScraper");


            });

            modelBuilder.Entity<Requisicao>(entity =>
            {
                entity.ToTable("Requisicao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.IdFuncionario).HasColumnName("id_funcionario");

                entity.Property(e => e.Prioridade)
                    .HasMaxLength(255)
                    .HasColumnName("prioridade");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Requisicaos)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Requisica__id_de__440B1D61");

                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Requisicaos)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Requisica__id_fu__44FF419A");


            });

            modelBuilder.Entity<ProdutoScraperModel>(entity =>
            {
                entity.ToTable("ProdutoScraper");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Loja)
                      .HasColumnName("Loja")
                      .HasColumnType("Int")
                      .IsRequired();

                entity.HasMany(p => p.Reports)
                      .WithOne(b => b.ProdutoScraperModel)
                      .HasForeignKey(sp => sp.ProdutoScraperModelId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Produto)
                      .WithOne(ps => ps.ProdutoScraperModel)
                      .HasForeignKey<ProdutoScraperModel>(ps => ps.IdProduto)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_ProdutoScraper_Produto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
