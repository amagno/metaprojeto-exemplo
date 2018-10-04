using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Commands.ProjectManagement;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Moq;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Application.Commands
{
  public class FinalizeProjectCommandTest
  {
    [Fact]
    public async Task Test_finalize_valid_project()
    {
      // criacao do commando
      var projectId = 0;
      var userIdentifier = Guid.NewGuid();
      var command = new FinalizeProjectCommand(projectId, userIdentifier, "info_test");

      // cria projectmanager e adiciona projeto valido
      var projectManager = new ProjectManager(userIdentifier);
      projectManager.AddProject("teste", DateTime.Now, DateTime.Now.AddDays(2));
      
      // mocks
      var mediatorMock = new Mock<IMediator>();
      var projectManagerRepositoryMock = new Mock<IProjectManagerRepository>();
      // setup do respoisotrio para retornar o project manager
      projectManagerRepositoryMock
        .Setup(r => r.GetByUserIdentifierAsync(userIdentifier))
        .ReturnsAsync(projectManager);
      // retornar true quando for feito commit
      projectManagerRepositoryMock
        .Setup(r => r.UnitOfWork.CommitAsync())
        .ReturnsAsync(true);
      // handler
      var handler = new FinalizeProjectCommandHandler(mediatorMock.Object, projectManagerRepositoryMock.Object);
      var result = await handler.Handle(command, It.IsAny<CancellationToken>());
      // projeto nao deve estar ativo
      Assert.False(projectManager.Projects.FirstOrDefault().IsActive);
      Assert.True(result);
    }
  }
}