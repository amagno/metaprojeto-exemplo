using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Commands.ProjectManagement;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.Events;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Moq;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Application.Commands
{
  public class CreateProjectCommandTest
  {

    /// <summary>
    /// Testa criar projeto com dados validos, deve retornar o id
    /// da entidade salva
    /// </summary>
    [Fact]
    public async Task Test_add_valid_project_with_project_manager_exists()
    {
      // guid do usuario logado
      var userIdentifier = Guid.NewGuid();
      // command
      var command = CreateFakeCommand(userIdentifier, DateTime.Now, DateTime.Now.AddDays(2));
      // mock do meidiator
      var mediatorMock = new Mock<IMediator>();
      // cria project manager
      var projectManager = new ProjectManager(userIdentifier);
      // mock do repositorio
      var projectManagerRepositoryMock = GetProjectManagerRepositoryMock();
      // faz com que o respositorio retorne o projectmanager criado
      projectManagerRepositoryMock
        .Setup(r => r.GetByUserIdentifierAsync(userIdentifier))
        .ReturnsAsync(projectManager);
      // handler
      var handler = new CreateProjectCommandHandler(mediatorMock.Object, projectManagerRepositoryMock.Object);
      var result = await handler.Handle(command, It.IsAny<CancellationToken>());

      // verifica se o evento foi publicado
      mediatorMock.Verify(m => m.Publish(
        It.IsAny<ProjectCreatedActionEvent>(), 
        It.IsAny<CancellationToken>()
      ), Times.Once());
      // deve conter um projeto dentro do projectmanager
      Assert.Single(projectManager.Projects);
      
      Assert.True(result);
    }
    /// <summary>
    /// Testa criar projeto com data invalida deve retornar uma exception
    /// do tipo invalid request
    /// </summary>
    [Fact]
    public async Task Test_create_invalid_date_project_shoudl_throw_exception()
    {
      var userIdentifier = Guid.NewGuid();
      // commando
      var command = CreateFakeCommand(userIdentifier, DateTime.Now, DateTime.Now.AddDays(-3));
      // Mock necessarios
      var mediatorMock = new Mock<IMediator>();
      var projectManagerRepositoryMock = GetProjectManagerRepositoryMock();
      // handler
      var handler = new CreateProjectCommandHandler(mediatorMock.Object, projectManagerRepositoryMock.Object);
      // assert exception
      await Assert.ThrowsAsync<InvalidRequestException>(() => 
        handler.Handle(command, It.IsAny<CancellationToken>())
      );
      // o evento de projeto nunca devera ser chamado no caso de erro
      mediatorMock.Verify(m => m.Publish(
        It.IsAny<ProjectCreatedActionEvent>(), 
        It.IsAny<CancellationToken>()
      ), Times.Never());
    }
    private CreateProjectCommand CreateFakeCommand(Guid userIdentifier, DateTime startDate, DateTime finishDate)
    {
      return new CreateProjectCommand("teste", startDate, finishDate, userIdentifier, "info_testing");
    }
    private Mock<IProjectManagerRepository> GetProjectManagerRepositoryMock()
    {
      var projectManagerRepositoryMock = new Mock<IProjectManagerRepository>();

      projectManagerRepositoryMock
        .Setup(r => r.UnitOfWork.CommitAsync())
        .ReturnsAsync(true);

      return projectManagerRepositoryMock;
    }
  }
}