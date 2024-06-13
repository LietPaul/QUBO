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
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=QUBO-db.mssql.somee.com;packet size=4096;user id=LietKynes_SQLLogin_1;pwd=dkaah9yxmn;data source=QUBO-db.mssql.somee.com;persist security info=False;initial catalog=QUBO-db;TrustServerCertificate=True");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arreglo>(entity =>
        {
            entity.HasKey(e => e.IdArreglo).HasName("PK__Arreglo__1DA8764EC7F32CD2");

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
            entity.Property(e => e.IdTecnico).HasColumnName("id_tecnico");
            entity.Property(e => e.Problema)
                .HasMaxLength(500)
                .HasColumnName("problema");
            entity.Property(e => e.Seña)
                .HasColumnType("money")
                .HasColumnName("seña");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");

            entity.HasOne(d => d.IdCelularNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdCelular)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Arreglo__id_celu__6477ECF3");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Arreglo__id_clie__6383C8BA");

            entity.HasOne(d => d.IdTecnicoNavigation).WithMany(p => p.Arreglos)
                .HasForeignKey(d => d.IdTecnico)
                .HasConstraintName("FK__Arreglo__id_tecn__656C112C");
        });

        modelBuilder.Entity<Celular>(entity =>
        {
            entity.HasKey(e => e.IdCelular).HasName("PK__Celular__3320C56CAB67A8D1");

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
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__677F38F5E81562AE");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
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
                .HasConstraintName("FK__Cliente__id_celu__5EBF139D");
        });

        modelBuilder.Entity<DetalleArreglo>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleA__4F1332DE6287F31E");

            entity.ToTable("DetalleArreglo");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdArreglo).HasColumnName("id_arreglo");
            entity.Property(e => e.IdParte).HasColumnName("id_parte");

            entity.HasOne(d => d.IdArregloNavigation).WithMany(p => p.DetalleArreglos)
                .HasForeignKey(d => d.IdArreglo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleAr__id_ar__68487DD7");

            entity.HasOne(d => d.IdParteNavigation).WithMany(p => p.DetalleArreglos)
                .HasForeignKey(d => d.IdParte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleAr__id_pa__693CA210");
        });

        modelBuilder.Entity<Parte>(entity =>
        {
            entity.HasKey(e => e.IdPartes).HasName("PK__Partes__B7D95BF08C34564D");

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partes__id_celul__5BE2A6F2");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.IdTecnico).HasName("PK__Tecnico__D550973746099EF1");

            entity.ToTable("Tecnico");

            entity.Property(e => e.IdTecnico).HasColumnName("id_tecnico");
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
                .HasMaxLength(256)
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
