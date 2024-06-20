using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QUBO.Models;

public partial class QuboDbContext : DbContext
{
    public QuboDbContext()
    {
    }

    public QuboDbContext(DbContextOptions<QuboDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Arreglo> Arreglos { get; set; }

    public virtual DbSet<Celular> Celulars { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleArreglo> DetalleArreglos { get; set; }

    public virtual DbSet<Parte> Partes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arreglo>(entity =>
        {
            entity.HasKey(e => e.IdArreglo).HasName("PK__Arreglo__1DA8764E255C203C");

            entity.ToTable("Arreglo");

            entity.Property(e => e.IdArreglo).HasColumnName("id_arreglo");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEnt)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ent");
            entity.Property(e => e.FechaIng)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ing");
            entity.Property(e => e.IdCelular).HasColumnName("id_celular");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Problema)
                .HasMaxLength(500)
                .HasColumnName("problema");
            entity.Property(e => e.Senia)
                .HasColumnType("money")
                .HasColumnName("senia");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");

            entity.HasOne(d => d.IdCelularNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdCelular)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Arreglo__id_celu__367C1819");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Arreglo__id_clie__3587F3E0");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Arreglo__id_usua__37703C52");
        });

        modelBuilder.Entity<Celular>(entity =>
        {
            entity.HasKey(e => e.IdCelular).HasName("PK__Celular__3320C56C186E2B0D");

            entity.ToTable("Celular");

            entity.Property(e => e.IdCelular).HasColumnName("id_celular");
            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(50)
                .HasColumnName("codigo_producto");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("modelo");
            entity.Property(e => e.PrecioUsd)
                .HasColumnType("money")
                .HasColumnName("precio_usd");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__677F38F581FBE9E0");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetalleArreglo>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleA__4F1332DE459F28E7");

            entity.ToTable("DetalleArreglo");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdArreglo).HasColumnName("id_arreglo");
            entity.Property(e => e.IdParte).HasColumnName("id_parte");

            entity.HasOne(d => d.IdArregloNavigation).WithMany(p => p.DetalleArreglos)
                .HasForeignKey(d => d.IdArreglo)
                .HasConstraintName("FK__DetalleAr__id_ar__3A4CA8FD");

            entity.HasOne(d => d.IdParteNavigation).WithMany(p => p.DetalleArreglos)
                .HasForeignKey(d => d.IdParte)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__DetalleAr__id_pa__3B40CD36");
        });

        modelBuilder.Entity<Parte>(entity =>
        {
            entity.HasKey(e => e.IdPartes).HasName("PK__Partes__B7D95BF0B7B82E6E");

            entity.Property(e => e.IdPartes).HasColumnName("id_partes");
            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(100)
                .HasColumnName("codigo_producto");
            entity.Property(e => e.IdCelular).HasColumnName("id_celular");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .HasColumnName("modelo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUsd)
                .HasColumnType("money")
                .HasColumnName("precio_usd");

            entity.HasOne(d => d.IdCelularNavigation).WithMany(p => p.Partes)
                .HasForeignKey(d => d.IdCelular)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Partes__id_celul__2A164134");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04ADB1A4B92A");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(256)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Dni)
                .HasMaxLength(50)
                .HasColumnName("dni");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.RolUsuario)
                .HasMaxLength(50)
                .HasColumnName("rol_usuario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
