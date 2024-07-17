using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUBO.Models;

public partial class Arreglo
{
    public long IdArreglo { get; set; }

    public long? IdCliente { get; set; }

    public long? IdCelular { get; set; }

    public string? Problema { get; set; }

    public DateTime? FechaIng { get; set; }

    public DateTime? FechaEnt { get; set; }
    public decimal? Senia { get; set; }
    public decimal? Total { get; set; }

    public long? IdUsuario { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleArreglo> DetalleArreglos { get; set; } = new List<DetalleArreglo>();

    public virtual Celular? IdCelularNavigation { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
