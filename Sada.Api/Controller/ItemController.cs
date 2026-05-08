using Microsoft.AspNetCore.Mvc;
using Sada.Application.DTOs;
using Sada.Application.Interfaces;
using Sada.Domain.Enums;

namespace Sada.Api.Controllers;

[ApiController]
[Route("api/itens")]
public class ItemController : ControllerBase
{
    private readonly ISadaService _service;

    public ItemController(
        ISadaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateItemDto dto)
    {
        var id =
            await _service.CreateAsync(dto);

        return Created("", id);
    }

    [HttpGet]
    public async Task<IActionResult> Get(Status? status, DateTime? dueDate)
    {
        var result =
            await _service.GetAsync(
                status,
                dueDate);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateItemDto dto)
    {
        await _service.UpdateAsync(
            id,
            dto);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        Guid id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}