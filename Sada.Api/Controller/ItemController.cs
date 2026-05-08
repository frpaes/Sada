using Microsoft.AspNetCore.Mvc;
using Sada.Application.DTOs;
using Sada.Application.Interfaces;
using Sada.Domain.Enums;

namespace Sada.Api.Controllers;

/// <summary>
/// API para gerenciamento de itens.
/// </summary>
[ApiController]
[Route("api/itens")]
public class ItemController : ControllerBase
{
    private readonly ISadaService _service;

    public ItemController(ISadaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria um novo item.
    /// </summary>
    /// <param name="item">Dados para criação.</param>
    /// <returns>Id do item criado.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateItemDto item)
    {
        try
        {
            var id = await _service.CreateAsync(item);

            return Created("", id);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Lista os itens com filtros opcionais.
    /// </summary>
    /// <param name="status">Status do item.
    /// Status do item:
    /// 1 = Pendente,
    /// 2 = Em progresso,
    /// 3 = Concluído
    /// </param>
    /// <param name="DataVencimento">Data de vencimento.</param>
    /// <returns>Lista de itens.</returns>
    [HttpGet]
    public async Task<IActionResult> Get(Status? status, DateTime? DataVencimento)
    {
        try
        {
            var result = await _service.GetAsync(
                status,
                DataVencimento);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Busca um item pelo identificador.
    /// </summary>
    /// <param name="id">Id do item.</param>
    /// <returns>Item encontrado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Success = false, ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Atualiza um item existente.
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <param name="item">
    /// Status do item:
    /// 1 = Pendente,
    /// 2 = Em progresso,
    /// 3 = Concluído
    /// </param>
    /// <returns>Lista de itens.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateItemDto item)
    {
        try
        {
            await _service.UpdateAsync(id, item);

            return Ok(new { Success = true, Message = "Registro atualizado com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Success = false, ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Success = false, ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { Success = false, ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Remove um item.
    /// </summary>
    /// <param name="id">Id do item.</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);

            return Ok(new { Success = true, Message = "Registro excluído com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Success = false,ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { Success = false,ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, ex.Message });
        }
    }
}