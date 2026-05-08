namespace Sada.Domain.Entities;

public class Log
{
    public int Id { get; set; }

    public string Metodo { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public int StatusCode { get; set; }

    public DateTime DataHora { get; set; }

}