using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturaArchivos.Entidades;

public class VentasDetalle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string CodProd { get; set; } = default!;

    public int Folio { get; set; }

    public int Cantidad { get; set; }

    public decimal Importe { get; set; } // No sobra tener redundancia, por si el precio cambia en el tiempo

    public decimal Subtotal { get; set; }

    [ForeignKey(nameof(CodProd))]
    public virtual Producto Producto { get; set; } = default!;

    [ForeignKey(nameof(Folio))]
    public virtual Venta Venta { get; set; } = default!;

}
