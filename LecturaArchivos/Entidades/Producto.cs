using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturaArchivos.Entidades;

public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string CodProd { get; set; } = default!;

    public string Descripcion { get; set; } = default!;

    public decimal Importe { get; set; }
}