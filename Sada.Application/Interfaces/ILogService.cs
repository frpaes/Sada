using Sada.Domain.Entities;

namespace Sada.Application.Interfaces;

public interface ILogService
{
    Task SalvarAsync(Log log);
}