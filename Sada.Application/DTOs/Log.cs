namespace Sada.Domain.Entities;

public class Log
{
    public int Id { get; set; }

    public string Metodo { get; set; }

    public string Json { get; set; }

    public int StatusCode { get; set; }

    public DateTime DataHora { get; set; }

}