using LecturaArchivos.Entidades;

namespace LecturaArchivos.Parsers;

public class Migrador
{
    public List<Cliente> Clientes { get; set; } = [];
    public List<Entidades.Venta> Ventas { get; set; } = [];
    public List<Entidades.Producto> Productos { get; set; } = [];
    public List<VentasDetalle> VentasDetalles { get; set; } = [];

    public static void Migrar(List<Venta> detallesVenta)
    {
        var migrador = new Migrador();
        foreach (var detalle in detallesVenta)
        {
            // Verificar si el cliente ya existe
            var cliente = migrador.Clientes.FirstOrDefault(c => c.CodCli == detalle.CodCli);
            if (cliente == null)
            {
                cliente = new Cliente
                {
                    CodCli = detalle.CodCli,
                    Nombre = detalle.Nombre
                };
                migrador.Clientes.Add(cliente);
            }

            var venta = new Entidades.Venta
            {
                Folio = detalle.Folio,
                CodCli = detalle.CodCli,
                Fecha = detalle.Fecha,
                Total = detalle.Total
            };
            migrador.Ventas.Add(venta);

            foreach (var producto in detalle.Productos)
            {
                // Verificar si el producto ya existe
                var prod = migrador.Productos.FirstOrDefault(p => p.CodProd == producto.CodProd);
                if (prod == null)
                {
                    prod = new Entidades.Producto
                    {
                        CodProd = producto.CodProd,
                        Descripcion = producto.Descripcion,
                        Importe = producto.Importe
                    };
                    migrador.Productos.Add(prod);
                }
                else if (prod.Importe != producto.Importe)
                {
                    // Actualizar el importe del producto si es diferente
                    prod.Importe = producto.Importe;
                }

                // Agregar detalle de venta
                migrador.VentasDetalles.Add(new VentasDetalle
                {
                    Folio = detalle.Folio,
                    CodProd = producto.CodProd,
                    Importe = producto.Importe,
                    Cantidad = producto.Cantidad,
                    Subtotal = producto.Subtotal
                });
            }
        }
        ToDB(migrador);
    }

    private static void ToDB(Migrador migrador)
    {
        using var db = new TiendaContext();

        Console.WriteLine("Eliminando todos los datos de la base de datos, si existen...");
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Console.WriteLine("Base de datos creada.");

        db.Clientes.AddRange(migrador.Clientes);
        db.Productos.AddRange(migrador.Productos);
        db.Ventas.AddRange(migrador.Ventas);
        db.VentasDetalles.AddRange(migrador.VentasDetalles);
        db.SaveChanges();
        Console.WriteLine("Datos migrados a la base de datos.");
    }
}