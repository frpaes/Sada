namespace Sada.Application.DTOs
{
    public class ResponseLog
    {
        public string Metodo { get; set; } = string.Empty;

        public string Endpoint { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public DateTime DataHora { get; set; }
    }
}
