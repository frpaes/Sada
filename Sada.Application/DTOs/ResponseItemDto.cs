using Sada.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sada.Application.DTOs
{
    public class ResponseItemDto
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataVencimento { get; set; }

        public Status Status { get; set; }
    }
}
