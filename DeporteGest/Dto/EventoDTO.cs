namespace DeporteGest.DTOs;

public class EventoDTO
{
    public int EventoId { get; set; }
    public string NombreEvento { get; set; }
    public DateOnly FechaEvento { get; set; }
    public string UbicacionEvento { get; set; }
}