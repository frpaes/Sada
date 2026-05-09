using FluentAssertions;
using Moq;
using Sada.Application.DTOs;
using Sada.Application.Service;
using Sada.Domain.Entities;
using Sada.Domain.Enums;
using Sada.Domain.Interfaces;

namespace Sada.Tests.Domain.Services;

public class SadaServiceTests
{
    private readonly Mock<ISadaRepository> _repository;
    private readonly SadaService _service;


    public SadaServiceTests()
    {
        _repository = new Mock<ISadaRepository>();
        _service = new SadaService(_repository.Object);
    }


    [Fact]
    public async Task CreateAsyncRetornaId()
    {
        var id = Guid.NewGuid();

        var entity = new Item
        {
            Id = id,
            Titulo = "Produto teste",
            Descricao = "Descrição teste",
            DataVencimento = DateTime.Now,
            Status = Status.EmProgresso
        };

        var dto = new CreateItemDto
        {
            Titulo = entity.Titulo,
            Descricao = entity.Descricao,
            DataVencimento = entity.DataVencimento,
            Status = entity.Status
        };

        _repository.Setup(x => x.CreateAsync(It.IsAny<Item>())).ReturnsAsync(entity);

        var result = await _service.CreateAsync(dto);
        entity.Id = result;

        result.Should().Be(id);

        _repository.Verify(x => x.CreateAsync(It.IsAny<Item>()), Times.Once);
    }


    [Fact]
    public async Task GetAsyncRetornaItens()
    {
        var entities = new List<Item> { new(), new() };

        _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(entities);

        var result = await _service.GetAsync(null, null);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }


    [Fact]
    public async Task GetByIdAsyncRetornaItem()
    {
        var id = Guid.NewGuid();

        var entity = new Item { Id = id };

        _repository.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult<Item?>(entity));

        var result = await _service.GetByIdAsync(id);
        
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
    }


    [Fact]
    public async Task UpdateAsync()
    {
        var id = Guid.NewGuid();

        var entity = new Item
        {
            Id = id,
            Titulo = "Antigo",
            Descricao = "Antigo"
        };

        var dto = new UpdateItemDto
        {
            Titulo = "Novo",
            Descricao = "Nova descrição",
            DataVencimento = DateTime.Now,
            Status = Status.EmProgresso
        };

        _repository.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult<Item?>(entity));

        await _service.UpdateAsync(id, dto);

        _repository.Verify(x => x.GetByIdAsync(id), Times.Once);

        _repository.Verify(x => x.UpdateAsync(It.Is<Item>(i => i.Id == id && i.Titulo == dto.Titulo && i.Descricao == dto.Descricao)), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync()
    {
        var id = Guid.NewGuid();

        var entity = new Item
        {
            Id = id,
            Titulo = "Produto teste",
            Descricao = "Descrição teste",
            DataVencimento = DateTime.Now,
            Status = Status.EmProgresso
        };

        _repository.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult<Item?>(entity));

         await _service.DeleteAsync(id);

        _repository.Verify(x => x.GetByIdAsync(id), Times.Once);

        _repository.Verify(x => x.DeleteAsync(It.Is<Item>(i => i.Id == id)), Times.Once);
    }


    [Fact]
    public async Task GetByIdAsyncRetornaItemNaoEncontrado()
    {
        var id = Guid.NewGuid();

        _repository.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult<Item?>(null));

        Func<Task> action = () => _service.GetByIdAsync(id);
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }
}