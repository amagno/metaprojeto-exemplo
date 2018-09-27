using System;
using System.Threading;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Services;
using MetaProjetoExemplo.Application.ViewModels;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Moq;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Application.Services
{
  public class ProjectManagementServiceTest
  {
    [Fact]
    public async Task Test_add_project_and_project_manager_if_project_manager_not_exists()
    {
      var user = new User("teste", "teste@teste.com", "123");
      var userRepositoryMock = new Mock<IUserRepository>();
      var uowMock = new Mock<IUnitOfWork>();

      userRepositoryMock
        .Setup(u => u.GetByIdentifierAsync(user.Identifier))
        .ReturnsAsync(user);

      var projectManagerRepositoryMock = new Mock<IProjectManagerRepository>();
      projectManagerRepositoryMock.SetupGet(c => c.UnitOfWork).Returns(uowMock.Object);

      var service = new ProjectManagementService(userRepositoryMock.Object, projectManagerRepositoryMock.Object);

      var data = new NewProject {
        Title =  "hello",
        StartDate = DateTime.Now,
        FinishDate = DateTime.Now.AddDays(3)
      };

      var result = await service.CreateProject(user.Identifier, data);

      projectManagerRepositoryMock.Verify(c => c.GetByUserIdentifierAsync(It.IsAny<Guid>()), Times.Once());
      projectManagerRepositoryMock.Verify(c => c.Add(It.IsAny<ProjectManager>()), Times.Once());
      uowMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

    }
    [Fact]
    public async Task Test_add_project_and_update_project_manager_if_project_manager_exists()
    {
      var user = new User("teste", "teste@teste.com", "123");
      var userRepositoryMock = new Mock<IUserRepository>();
      var uowMock = new Mock<IUnitOfWork>();

      userRepositoryMock
        .Setup(u => u.GetByIdentifierAsync(user.Identifier))
        .ReturnsAsync(user);

      var projectManagerRepositoryMock = new Mock<IProjectManagerRepository>();
      projectManagerRepositoryMock.Setup(c => c.GetByUserIdentifierAsync(It.IsAny<Guid>())).ReturnsAsync(new ProjectManager(Guid.NewGuid()));
      projectManagerRepositoryMock.SetupGet(c => c.UnitOfWork).Returns(uowMock.Object);

      var service = new ProjectManagementService(userRepositoryMock.Object, projectManagerRepositoryMock.Object);

      var data = new NewProject {
        Title =  "hello",
        StartDate = DateTime.Now,
        FinishDate = DateTime.Now.AddDays(3)
      };

      var result = await service.CreateProject(user.Identifier, data);

      projectManagerRepositoryMock.Verify(c => c.GetByUserIdentifierAsync(It.IsAny<Guid>()), Times.Once());
      projectManagerRepositoryMock.Verify(c => c.Add(It.IsAny<ProjectManager>()), Times.Never());
      projectManagerRepositoryMock.Verify(c => c.Update(It.IsAny<ProjectManager>()), Times.Once());
      uowMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

    }
  }
}