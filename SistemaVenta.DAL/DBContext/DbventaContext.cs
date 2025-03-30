using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.dbcontext
{

    public partial class DbventaContext : DbContext
    {
        public DbventaContext()
        {
        }

        public DbventaContext(DbContextOptions<DbventaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MenuRol> MenuRols { get; set; }

        public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }

        public virtual DbSet<Producto> Productos { get; set; }

        public virtual DbSet<Proveedor> Proveedors { get; set; }

        public virtual DbSet<Reparaciones> Reparaciones { get; set; }

        public virtual DbSet<Rol> Rols { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VentasEliminada> VentaEliminada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240CC5483E61");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
                entity.Property(e => e.EsActivo)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("esActivo");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<VentasEliminada>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__VentasEl__3214EC0795C40411");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");
                entity.Property(e => e.MotivoEliminacion).HasMaxLength(500);
            });
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__885457EE699505F5");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");
                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");
                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");
                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion");
                entity.Property(e => e.EsActivo)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("esActivo");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombres");
                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F36CB4AE7");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
                entity.Property(e => e.IdCliente).HasColumnName("idCliente");
                entity.Property(e => e.IdProducto).HasColumnName("idProducto");
                entity.Property(e => e.IdReparacion).HasColumnName("idReparacion");
                entity.Property(e => e.IdVenta).HasColumnName("idVenta");
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__DetalleVe__idCli__151B244E");

                entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleVe__idPro__46E78A0C");

                entity.HasOne(d => d.IdReparacionNavigation).WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdReparacion)
                    .HasConstraintName("FK__DetalleVe__idRep__71D1E811");

                entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__idVen__45F365D3");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF4836CCD349B");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");
                entity.Property(e => e.Icono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("icono");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<MenuRol>(entity =>
            {
                entity.HasKey(e => e.IdMenuRol).HasName("PK__MenuRol__9D6D61A48EC68209");

                entity.ToTable("MenuRol");

                entity.Property(e => e.IdMenuRol).HasColumnName("idMenuRol");
                entity.Property(e => e.IdMenu).HasColumnName("idMenu");
                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK__MenuRol__idMenu__2E1BDC42");

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__MenuRol__idRol__2F10007B");
            });

            modelBuilder.Entity<NumeroDocumento>(entity =>
            {
                entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__NumeroDo__471E421A1B90FADC");

                entity.ToTable("NumeroDocumento");

                entity.Property(e => e.IdNumeroDocumento).HasColumnName("idNumeroDocumento");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_numero");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132C06C8B04");

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");
                entity.Property(e => e.EsActivo)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("esActivo");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precioVenta");
                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Producto__idCate__3A81B327");

                entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK__Producto__idProv__04E4BC85");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6BBB5D906F");

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
                entity.Property(e => e.ContactoPersona)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contactoPersona");
                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");
                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion");
                entity.Property(e => e.EsActivo)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("esActivo");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.NombreEmpresa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreEmpresa");
                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Reparaciones>(entity =>
            {
                entity.HasKey(e => e.IdReparacion).HasName("PK__Reparaci__4E5D15A6CAA3122A");

                entity.Property(e => e.IdReparacion).HasColumnName("idReparacion");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Reparaciones)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Reparacio__idCat__6FE99F9F");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Idrol).HasName("PK__Rol__24C6BB2079A9AAAA");

                entity.ToTable("Rol");

                entity.Property(e => e.Idrol).HasColumnName("idrol");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A66B5A0AE5");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
                entity.Property(e => e.Clave)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("clave");
                entity.Property(e => e.Correo)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("correo");
                entity.Property(e => e.EsActivo)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("esActivo");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.IdRol).HasColumnName("idRol");
                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombreCompleto");

                entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__idRol__31EC6D26");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D5614A5AB8965");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
               //entity.Property(e => e.IdClienteVenta).HasColumnName("idCliente_Venta");
                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");
                entity.Property(e => e.TipoPago)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoPago");
                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

             /*   entity.HasOne(d => d.IdClienteVentaNavigation).WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdClienteVenta)
                    .HasConstraintName("FK_detalleventa_Cliente");
             */
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}