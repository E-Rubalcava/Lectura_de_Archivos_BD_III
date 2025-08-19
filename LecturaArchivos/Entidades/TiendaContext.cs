using LecturaArchivos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LecturaArchivos.Entidades;

public class TiendaContext() : DbContext()
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<VentasDetalle> VentasDetalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Tienda;User Id=sa;Password=Password1234;TrustServerCertificate=true;");
    }

}