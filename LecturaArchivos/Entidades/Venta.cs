using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturaArchivos.Entidades;

public class Venta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Folio { get; set; }

    public DateOnly Fecha { get; set; }

    public string CodCli { get; set; } = default!;

    public decimal Total { get; set; }

    public virtual List<VentasDetalle> VentasDetalles { get; } = [];

    [ForeignKey(nameof(CodCli))]
    public virtual Cliente Cliente { get; set; } = default!;
}