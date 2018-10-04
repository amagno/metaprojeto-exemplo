using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Queries;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace MetaProjetoExemplo.FunctionalTests.ProjectManagement
{
  [Collection("ProjectManagement")]
  public class GetUserProjectsTest : IClassFixture<ProjectManagementWebApplication>
  {
    private readonly ProjectManagementWebApplication _webApplicationFactory;

    public GetUserProjectsTest(ProjectManagementWebApplication webApplicationFactory)
    {
      _webApplicationFactory = webApplicationFactory;
    }
    /// <summary>
    /// Testa consulta dos projetos do gerente de projetos
    /// </summary>
    [Fact]
    public async Task Test_get_user_projects()
    {
      using (var scope = _webApplicationFactory.CreateScope())
      {
        var client = await _webApplicationFactory.CreateAuthenticatedClientForDefaultUserAsync(scope);
        var ef = _webApplicationFactory.GetContext(scope);
        // pega default user
        var user = await ef.Users.FirstAsync();
        // adiciona dados
        var projectManager = new ProjectManager(user.Identifier);
        projectManager.AddProject("test1", DateTime.Now, DateTime.Now.AddDays(2));
        ef.ProjectManagers.Add(projectManager);
        await ef.SaveChangesAsync();
        // realiza requisição
        var response = await client.GetAsync("/api/project-management/");
        response.EnsureSuccessStatusCode();
        // resultadp
        var result = await response.Content.ReadAsStringAsync();
        var objectResult = JsonConvert.DeserializeObject<ProjectManagerViewModel>(result);
        // deve conter um projeto
        Assert.Single(objectResult.Projects);
      }
    }
  }
}