using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUBO.Models;

public partial class Usuario
{
    public long IdUsuario { get; set; }

    public string? Nombre { get; set; }

    [Display(Name = "Contraseña")]
    public string? Contrasenia { get; set; }

    [Display(Name = "Rol")]
    public string? RolUsuario { get; set; }

    public string? Apellido { get; set; }

    public string? Dni { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Arreglo> Arreglos { get; set; } = new List<Arreglo>();
}
