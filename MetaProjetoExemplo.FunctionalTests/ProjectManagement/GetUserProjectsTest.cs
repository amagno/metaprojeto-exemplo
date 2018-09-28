using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Queries;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace MetaProjetoExemplo.FunctionalTests.ProjectManagement
{
  public class GetUserProjectsTest : IClassFixture<ProjectManagementWebApplication>
  {
    private readonly ProjectManagementWebApplication _webApplicationFactory;

    public GetUserProjectsTest(ProjectManagementWebApplication webApplicationFactory)
    {
      _webApplicationFactory = webApplicationFactory;
    }
    /// <summary>
    /// Detalhe: para realizar migração no banco de dados na memoria as configuraçoes
    /// do entity framework nao pode ter funções ligadas ao banco de dados
    /// pois o banco de dados na memoria não possui funçoões por exemplo do SQLServer
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_get_user_projects()
    {
      using (var scope = _webApplicationFactory.CreateScope())
      {
        var client = await _webApplicationFactory.CreateAuthenticatedClientForDefaultUserAsync(scope);
        var ef = _webApplicationFactory.GetContext(scope);
        var user = await ef.Users.FirstAsync();
        // dados da requsição
        var projectManager = new ProjectManager(user.Identifier);
        projectManager.AddProject("test1", DateTime.Now, DateTime.Now.AddDays(2));

        ef.ProjectManagers.Add(projectManager);
        await ef.SaveChangesAsync();

        var projects = await ef.Projects.ToListAsync();
        Assert.Single(projects);
        // realiza requisição
        var response = await client.GetAsync("/api/project-management/");
        // resultadp
        var result = await response.Content.ReadAsStringAsync();
        var objectResult = JsonConvert.DeserializeObject<ProjectManagerViewModel>(result);

        response.EnsureSuccessStatusCode();
        Assert.Single(objectResult.Projects);
      }
    }
  }
}