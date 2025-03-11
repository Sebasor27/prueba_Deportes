using System;
using System.Collections.Generic;

namespace DeporteGest.Models;

public partial class Inscripcione
{
    public int InscripcionId { get; set; }

    public int? EventoId { get; set; }

    public int? ParticipanteId { get; set; }

    public DateTime FechaInscripcion { get; set; }

    public virtual Evento? Evento { get; set; }

    public virtual Participante? Participante { get; set; }
}
