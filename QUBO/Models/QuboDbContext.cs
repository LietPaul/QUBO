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

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=QUBO-db.mssql.somee.com;packet size=4096;user id=LietKynes_SQLLogin_1;pwd=dkaah9yxmn;data source=QUBO-db.mssql.somee.com;persist security info=False;initial catalog=QUBO-db;TrustServerCertificate=True");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arreglo>(entity =>
        {
            entity.HasKey(e => e.IdArreglo);

            entity.ToTable("Arreglo");

            entity.Property(e => e.IdArreglo)
                .ValueGeneratedNever()
                .HasColumnName("id_arreglo");
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
            entity.Property(e => e.IdTecnico).HasColumnName("id_tecnico");
            entity.Property(e => e.PagoTotal)
                .HasColumnType("money")
                .HasColumnName("pago_total");
            entity.Property(e => e.Problema)
                .HasMaxLength(500)
                .HasColumnName("problema");
            entity.Property(e => e.Seña)
                .HasColumnType("money")
                .HasColumnName("seña");

            entity.HasOne(d => d.IdCelularNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdCelular)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arreglo_Celular");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arreglo_Cliente");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdTecnico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Arreglo_Tecnico");
        });

        modelBuilder.Entity<Celular>(entity =>
        {
            entity.HasKey(e => e.IdCelular);

            entity.ToTable("Celular");

            entity.Property(e => e.IdCelular)
                .ValueGeneratedNever()
                .HasColumnName("id_celular");
            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(50)
                .HasColumnName("codigo_producto");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .HasColumnName("modelo");
            entity.Property(e => e.PrecioUsd)
                .HasColumnType("money")
                .HasColumnName("precio_usd");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IdCelular).HasColumnName("id_celular");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdCelularNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdCelular)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Celular");
        });

        modelBuilder.Entity<DetalleArreglo>(entity =>
        {
            entity.HasKey(e => e.IdDetalle);

            entity.ToTable("DetalleArreglo");

            entity.Property(e => e.IdDetalle)
                .ValueGeneratedNever()
                .HasColumnName("id_detalle");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdArreglo).HasColumnName("id_arreglo");
            entity.Property(e => e.IdParte).HasColumnName("id_parte");

            entity.HasOne(d => d.IdArregloNavigation).WithMany(p => p.DetalleArreglos)
                .HasForeignKey(d => d.IdArreglo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleArreglo_Arreglo");

            entity.HasOne(d => d.IdParteNavigation).WithMany(p => p.DetalleArreglos)
                .HasForeignKey(d => d.IdParte)
                .HasConstraintName("FK_DetalleArreglo_Partes");
        });

        modelBuilder.Entity<Parte>(entity =>
        {
            entity.HasKey(e => e.IdParte);

            entity.Property(e => e.IdParte)
                .ValueGeneratedNever()
                .HasColumnName("id_parte");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partes_Celular");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.IdTecnico);

            entity.ToTable("Tecnico");

            entity.Property(e => e.IdTecnico)
                .ValueGeneratedNever()
                .HasColumnName("id_tecnico");
            entity.Property(e => e.Dni)
                .HasMaxLength(50)
                .HasColumnName("dni");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD083D13A0");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(100)
                .HasColumnName("contrasenia");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.RolUsuario)
                .HasMaxLength(50)
                .HasColumnName("rol_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
