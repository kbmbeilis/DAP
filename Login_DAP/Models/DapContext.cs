using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Login_DAP.Models;

public partial class DapContext : DbContext
{
    public DapContext()
    {
    }

    public DapContext(DbContextOptions<DapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dependencia> Dependencias { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Unidad> Unidades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=LAPTOP-D6S4JT0D\\MSSQLSERVER01;Database=DAP;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dependencia>(entity =>
        {
            entity.HasKey(e => e.IdDependencia);

            entity.Property(e => e.NombreDependencia).HasColumnType("text");
            entity.Property(e => e.Estatus);
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.IdFuncionario);

            entity.HasIndex(e => e.IdUnidad, "IX_Funcionarios_IdUnidad");

            entity.Property(e => e.Email).HasColumnType("text");
            entity.Property(e => e.FechaCargo).HasColumnType("text");
            entity.Property(e => e.IdUnidad).HasDefaultValueSql("((1))");
            entity.Property(e => e.NombreFuncionario);
            entity.Property(e => e.PuestoFuncional).HasColumnType("text");
            entity.Property(e => e.PuestoOficial).HasColumnType("text");
            entity.Property(e => e.Telefono).HasColumnType("text");

            entity.HasOne(d => d.Unidad).WithMany(p => p.Funcionarios).HasForeignKey(d => d.IdUnidad);
        });

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.HasKey(e => e.IdUnidad);

            entity.HasIndex(e => e.IdDependencia, "IX_Unidades_IdDependencia");

            entity.Property(e => e.Ciudad)
                .HasDefaultValueSql("('HERMOSILLO')")
                .HasColumnType("text");
            entity.Property(e => e.CodigoPostal).HasDefaultValueSql("((83000))");
            entity.Property(e => e.Colonia).HasColumnType("text");
            entity.Property(e => e.Domicilio).HasColumnType("text");
            entity.Property(e => e.EmailUnidad)
                .HasDefaultValueSql("('NOESPECIFIC@DO.COM')")
                .HasColumnType("text");
            entity.Property(e => e.Entidad)
                .HasDefaultValueSql("('SONORA')")
                .HasColumnType("text");
            entity.Property(e => e.IdDependencia).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NombreUnidad).HasColumnType("text");
            entity.Property(e => e.Pais)
                .HasDefaultValueSql("('MEXICO')")
                .HasColumnType("text");
            entity.Property(e => e.Telefonos)
                .HasDefaultValueSql("('NOESPECIFICADOS')")
                .HasColumnType("text");

            entity.HasOne(d => d.Dependencia).WithMany(p => p.Unidades).HasForeignKey(d => d.IdDependencia);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
