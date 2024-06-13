using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class DetalleArreglo
{
    public long IdDetalle { get; set; }

    public long IdArreglo { get; set; }

    public long IdParte { get; set; }

    public string? Descripcion { get; set; }

    public virtual Arreglo IdArregloNavigation { get; set; } = null!;

    public virtual Parte IdParteNavigation { get; set; } = null!;
}
