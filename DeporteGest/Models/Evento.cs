using System;
using System.Collections.Generic;

namespace DeporteGest.Models;

public partial class Evento
{
    public int EventoId { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Ubicacion { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Inscripcione> Inscripciones { get; set; } = new List<Inscripcione>();
}
