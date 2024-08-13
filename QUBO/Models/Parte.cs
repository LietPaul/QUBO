using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUBO.Models;

public partial class Parte
{
    public long IdPartes { get; set; }

    public string? Nombre { get; set; }

    public string? Modelo { get; set; }

    [Display(Name = "Precio")]
    public decimal? PrecioUsd { get; set; }

    [Display(Name = "Codigo")]
    public string? CodigoProducto { get; set; }

    public long? IdCelular { get; set; }

    public virtual ICollection<DetalleArreglo> DetalleArreglos { get; set; } = new List<DetalleArreglo>();

    public virtual Celular? IdCelularNavigation { get; set; }
}
