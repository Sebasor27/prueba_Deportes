namespace DeporteGest.DTOs;

public class InscripcionDTO
{
    public int InscripcionId { get; set; }
    public DateTime FechaInscripcion { get; set; }
    
    // Datos del Evento
    public int EventoId { get; set; }
    public string NombreEvento { get; set; } = null!;
    public DateOnly FechaEvento { get; set; }
    public string UbicacionEvento { get; set; } = null!;
    
    // Datos del Participante
    public int ParticipanteId { get; set; }
    public string NombreParticipante { get; set; } = null!;
    public string ApellidoParticipante { get; set; } = null!;
    public string EmailParticipante { get; set; } = null!;
}
