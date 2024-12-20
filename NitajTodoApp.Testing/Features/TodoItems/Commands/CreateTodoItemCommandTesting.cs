using FluentAssertions;
using MapsterMapper;
using Moq;
using NitajTodoApp.Application.Features.TodoItems.Commands.Create;
using NitajTodoApp.Domain.Entities;
using NitajTodoApp.Domain.Repositories;

namespace NitajTodoApp.Testing.Features.TodoItems.Commands;

public class CreateTodoItemCommandTesting
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IGenericRepository<TodoItem>> _todoItemRepositoryMock;


    public CreateTodoItemCommandTesting()
    {
        _mapperMock = new Mock<IMapper>();
        _todoItemRepositoryMock = new Mock<IGenericRepository<TodoItem>>();
    }

    [Fact]
    public async Task Handle_ShouldCreateTodoItemAndReturnSuccess()
    {
        // Arrange
        var command = new CreateTodoItemCommand { Title = "Test Todo Item" };
        var todoItem = new TodoItem { Title = command.Title };

        // Set up mapper mock
        _mapperMock
            .Setup(m => m.Map<TodoItem>(command))
            .Returns(todoItem);

        // Set up repository mocks
        _todoItemRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _todoItemRepositoryMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        var handler = new CreateTodoItemCommandHandler(
            _mapperMock.Object,
            _todoItemRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _mapperMock.Verify(m => m.Map<TodoItem>(command), Times.Once);
        _todoItemRepositoryMock.Verify(r => r.AddAsync(It.Is<TodoItem>(t => t.Title == command.Title), It.IsAny<CancellationToken>()), Times.Once);
        _todoItemRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
