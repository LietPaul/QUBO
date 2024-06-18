using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Arreglo
{
    public long IdArreglo { get; set; }

    public long IdCliente { get; set; }

    public long IdCelular { get; set; }

    public string? Problema { get; set; }

    public DateTime? FechaIng { get; set; }

    public DateTime? FechaEnt { get; set; }

    public decimal? Seña { get; set; }

    public decimal? Total { get; set; }

    public long? IdTecnico { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleArreglo> DetalleArreglos { get; set; } = new List<DetalleArreglo>();

    public virtual Celular IdCelularNavigation { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Tecnico? IdTecnicoNavigation { get; set; }
}
