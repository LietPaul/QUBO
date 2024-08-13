using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QUBO.Models;

public partial class DetalleArreglo
{
    public long IdDetalle { get; set; }

    public long? IdArreglo { get; set; }

    public long? IdParte { get; set; }

    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    public virtual Arreglo? IdArregloNavigation { get; set; }

    public virtual Parte? IdParteNavigation { get; set; }
}
