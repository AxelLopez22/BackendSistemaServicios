using System;
using System.Collections.Generic;
using ApiServicios.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiServicios.Models
{
    public partial class sistemaserviciosContext : DbContext
    {
        public sistemaserviciosContext()
        {
        }

        public sistemaserviciosContext(DbContextOptions<sistemaserviciosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClienteServicio> ClienteServicios { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Plane> Planes { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<PagosClientesSP> PagosClientesSP { get; set; } = null!;
        public virtual DbSet<VwListarCliente> VwListarClientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Inss)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("INSS");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClienteServicio>(entity =>
            {
                entity.ToTable("ClienteServicio");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.ClienteServicios)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("Fk_ClienteServicio_Refe_Cliente");

                entity.HasOne(d => d.IdPlanNavigation)
                    .WithMany(p => p.ClienteServicios)
                    .HasForeignKey(d => d.IdPlan)
                    .HasConstraintName("Fk_ClienteServicio_Refe_Plan");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ClienteServicios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_ClienteServicio_Refe_Usuario");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Mes)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteServicioNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdClienteServicio)
                    .HasConstraintName("Fk_Pagos_Refe_ClienteServicio");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Planes)
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("Fk_Planes_References_Servicio");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwListarCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_ListarClientes");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(101)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
