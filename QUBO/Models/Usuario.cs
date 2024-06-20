using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Usuario
{
    public long IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Contrasenia { get; set; }

    public string? RolUsuario { get; set; }

    public string? Apellido { get; set; }

    public string? Dni { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Arreglo> Arreglos { get; set; } = new List<Arreglo>();
}
