using Xunit;
using MetaProjetoExemplo.Domain.ProjectManagement;
using System;

namespace MetaProjetoExemplo.UnitTests.Domain.ProjectManagement
{
  public class ProjectManagerTest
  {
    /// <summary>
    /// Testa adicionar Projects ao ProjectManager
    /// </summary>
    [Fact]
    public void Test_add_valid_project()
    {
      //Given
      var projectManager = new ProjectManager(Guid.NewGuid());
      //When
      projectManager.AddProject("teste 1", DateTime.Now, DateTime.Now.AddDays(10));
      projectManager.AddProject("teste 2", DateTime.Now.AddDays(11), DateTime.Now.AddDays(21));
    
      //Then
      Assert.Equal(2, projectManager.Projects.Count);
    }

    /// <summary>
    /// Testa tentativa de adicionar projetos com datas inválidas deve ser lançado exception
    /// </summary>
    [Fact]
    public void Test_add_invalid_date_project_should_throw_exception()
    {
      // given
      var projectManager = new ProjectManager(Guid.NewGuid());
      // projeto valido
      projectManager.AddProject("teste 1", DateTime.Now, DateTime.Now.AddDays(10));
      // projeto com data final anterior ao começo
      Assert.Throws<InvalidProjectDateDomainException>(() => {
        projectManager.AddProject("teste 2", DateTime.Now, DateTime.Now.AddDays(-10));
      });
      // projeto invaldo: fica entre o projeto valido "teste 1"
      Assert.Throws<InvalidProjectDateDomainException>(() => {
        projectManager.AddProject("teste 3", DateTime.Now.AddDays(2), DateTime.Now.AddDays(4));
      });
      // projeto invaldo: data de começo fica entre o projeto valido "teste 1"
      Assert.Throws<InvalidProjectDateDomainException>(() => {
        projectManager.AddProject("teste 4", DateTime.Now.AddDays(2), DateTime.Now.AddDays(20));
      });
      // projeto invaldo: data de final fica entre o projeto valido "teste 1"
      Assert.Throws<InvalidProjectDateDomainException>(() => {
        projectManager.AddProject("teste 4", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(2));
      });
    }
  }
}