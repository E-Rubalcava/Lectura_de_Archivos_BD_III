using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturaArchivos.Entidades;

public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string CodCli { get; set; } = default!;

    public string Nombre { get; set; } = default!;

    public virtual List<Venta> Ventas { get; } = [];

}