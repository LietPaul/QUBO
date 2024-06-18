using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Celular
{
    public long IdCelular { get; set; }

    public string? Modelo { get; set; }

    public string? Marca { get; set; }

    public string? CodigoProducto { get; set; }

    public decimal? PrecioUsd { get; set; }

    public virtual ICollection<Arreglo> Arreglos { get; set; } = new List<Arreglo>();

    public virtual ICollection<Parte> Partes { get; set; } = new List<Parte>();
}
