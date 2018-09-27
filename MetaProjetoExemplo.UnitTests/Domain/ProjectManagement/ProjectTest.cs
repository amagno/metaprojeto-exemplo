using System;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Domain.ProjectManagement
{
  public class ProjectTest
  {
    /// <summary>
    /// Testa criar projeto com data de finalizar anterior a data de começo deve gerar erro
    /// </summary>
    [Fact]
    public void Test_create_project_wtih_invalid_date()
    {
      Assert.Throws<InvalidProjectDateException>(() => {
        new Project(1, "teste", DateTime.Now, DateTime.Now.AddDays(-1));
      });
    }
    /// <summary>
    /// Testa finalizar um projeto com data valida, ou seja a data já está expirada
    /// </summary>
    [Fact]
    public void Test_finalize_valid_date()
    {
      var project = new Project(1, "teste", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-1));
      Assert.True(project.IsActive);
      project.FinalizeThis();
      Assert.False(project.IsActive);
    }
    /// <summary>
    /// Testa finalizar projeto com data invalida ou seja a data ainda não foi expirada
    /// </summary>
    [Fact]
    public void Test_finalize_invalid_date_should_throw_exception()
    {
      var project = new Project(1, "teste", DateTime.Now, DateTime.Now.AddDays(3));

      Assert.Throws<InvalidFinalizeProjectException>(() => {
        project.FinalizeThis();
      });
    }
  }
}