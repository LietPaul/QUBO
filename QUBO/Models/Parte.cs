using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Parte
{
    public long IdPartes { get; set; }

    public string? Nombre { get; set; }

    public string? Modelo { get; set; }

    public decimal? PrecioUsd { get; set; }

    public string? CodigoProducto { get; set; }

    public long IdCelular { get; set; }

    public virtual ICollection<DetalleArreglo> DetalleArreglos { get; set; } = new List<DetalleArreglo>();

    public virtual Celular IdCelularNavigation { get; set; } = null!;
}
