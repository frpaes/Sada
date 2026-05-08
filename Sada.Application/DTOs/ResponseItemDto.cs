using Sada.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sada.Application.DTOs
{
    public class ResponseItemDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public Status Status { get; set; }
    }
}
