using System;
using System.Collections.Generic;

namespace QUBO.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string? RolUsuario { get; set; }
}
