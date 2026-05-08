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

    /// <summary>
    /// Cria um novo item.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateItemDto item)
    {
        try
        {
            var id =await _service.CreateAsync(item);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new
                {
                    Success = true,
                    Message = "Registro criado com sucesso.",
                    Id = id
                });
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
            return StatusCode(500, new { Success = false, ex.Message });
        }
    }

    /// <summary>
    /// Lista os itens com filtros opcionais.
    /// </summary>
    /// <param name="status">
    /// 1 = Pendente,
    /// 2 = Em progresso,
    /// 3 = Concluído
    /// </param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(
        Status? status,
        DateTime? dataVencimento)
    {
        try
        {
            var result = await _service.GetAsync(status,dataVencimento);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {Success = false,ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false,ex.Message });
        }
    }

    /// <summary>
    /// Busca item pelo Id.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try { 

            var result = await _service.GetByIdAsync(id);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Success = false, ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, ex.Message });
        }
    }

    /// <summary>
    /// Atualiza um item.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update( Guid id, [FromBody] UpdateItemDto item)
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
            return BadRequest(new { Success = false,ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { Success = false, ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, ex.Message });
        }
    }

    /// <summary>
    /// Remove um item.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try {
            
            await _service.DeleteAsync(id);

            return Ok(new { Success = true, Message = "Registro excluído com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Success = false, ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { Success = false, ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, ex.Message });
        }
    }
}