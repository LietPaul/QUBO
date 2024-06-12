using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Cliente
{
    public long IdCliente { get; set; }

    public long IdCelular { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Dni { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Arreglo> Arreglos { get; set; } = new List<Arreglo>();

    public virtual Celular IdCelularNavigation { get; set; } = null!;
}
