using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Tecnico
{
    public int IdTecnico { get; set; }

    public string? Nombre { get; set; }

    public string? Dni { get; set; }

    public virtual ICollection<Arreglo> Arreglos { get; set; } = new List<Arreglo>();
}
