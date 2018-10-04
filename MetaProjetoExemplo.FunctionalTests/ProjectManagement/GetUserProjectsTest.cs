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
        response.EnsureSuccessStatusCode();
        // resultadp
        var result = await response.Content.ReadAsStringAsync();
        var objectResult = JsonConvert.DeserializeObject<ProjectManagerViewModel>(result);

        Assert.Single(objectResult.Projects);
      }
    }
  }
}