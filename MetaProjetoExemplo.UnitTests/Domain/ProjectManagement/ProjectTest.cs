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
      Assert.Throws<InvalidProjectDateDomainException>(() => {
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
      project.FinalizeNow();
      Assert.False(project.IsActive);
    }
  }
}