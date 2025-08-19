using System.Globalization;
using System.Text.Json;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.Configuration;

namespace LecturaArchivos.Parsers;

/// <summary>
/// Representa una venta para el formato JSON y XML
/// </summary>
public class Venta
{
    public int Folio { get; set; }
    public DateOnly Fecha { get; set; }
    public string CodCli { get; set; } = default!;
    public string Nombre { get; set; } = default!;

    [XmlArray("Productos")]
    [XmlArrayItem("Producto")]
    public List<Producto> Productos { get; set; } = [];
    public decimal Total { get; set; }
}

/// <summary>
/// Representa un producto para el formato JSON y XML
/// </summary>
public class Producto
{
    public string CodProd { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public int Cantidad { get; set; }
    public decimal Importe { get; set; }
    public decimal Subtotal { get; set; }
}

/// <summary>
/// Representa la raíz de las ventas para el formato XML
/// </summary>
[XmlRoot("Ventas")]
public class Ventas
{
    [XmlElement("Venta")]
    public List<Venta> ListaVentas { get; set; } = [];
}

/// <summary>
/// Representa una linea de venta para el formato TXT
/// </summary>
public class LineaVenta
{
    public int Folio { get; set; }
    public DateOnly Fecha { get; set; }
    public string CodCli { get; set; } = default!;
    public string Nombre { get; set; } = default!;
    public int Cantidad { get; set; }
    public string CodProd { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public decimal Importe { get; set; }
    public decimal Subtotal { get; set; }
}

public static class Parser
{
    public static List<Venta> ParseFromJson(string json)
    {
        var deserialized = JsonSerializer.Deserialize<List<Venta>>(json);
        if (deserialized == null)
            return [];
        return deserialized;
    }

    public static List<Venta> ParseFromXml(string xml)
    {
        // Lógica para deserializar XML a objetos
        XmlSerializer serializer = new(typeof(Ventas));
        using StringReader reader = new(xml);
        var ventas = (Ventas?)serializer.Deserialize(reader);
        if (ventas == null)
            return [];
        return ventas.ListaVentas;
    }

    public static List<Venta> ParseFromTxt(string txt)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = "|",
            // Ignorar comillas
            Quote = '\0',
            Mode = CsvMode.NoEscape
        };
        using var csvReader = new CsvReader(new StringReader(txt), config);
        var registros = csvReader.GetRecords<LineaVenta>();

        // Agrupar registros por venta, y convertir a objetos Venta
        var ventas = registros.GroupBy(r => new { r.Folio, r.Fecha, r.CodCli, r.Nombre })
            .Select(g => new Venta
            {
                Folio = g.Key.Folio,
                Fecha = g.Key.Fecha,
                CodCli = g.Key.CodCli,
                Nombre = g.Key.Nombre,
                Productos = [.. g.Select(p => new Producto
                {
                    CodProd = p.CodProd,
                    Descripcion = p.Descripcion,
                    Cantidad = p.Cantidad,
                    Importe = p.Importe,
                    Subtotal = p.Subtotal
                })],
                Total = g.Sum(p => p.Subtotal)
            }).ToList();

        return ventas;
    }

}
