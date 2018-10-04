using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MetaProjetoExemplo.Api;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.ProjectManagement;
using MetaProjetoExemplo.Infrastructure;
using MetaProjetoExemplo.Security;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace MetaProjetoExemplo.FunctionalTests.ProjectManagement
{
  [Collection("ProjectManagement")]
  public class CreateProjectTest : IClassFixture<ProjectManagementWebApplication>
  {
    private readonly ProjectManagementWebApplication _webApplicationFactory;
    public CreateProjectTest(ProjectManagementWebApplication webApplicationFactory)
    {
      _webApplicationFactory = webApplicationFactory;
    }
    /// <summary>
    /// Testa criar projeto com dados validos
    /// </summary>
    [Fact]
    public async Task Test_create_project_with_valid_data()
    {
      using (var scope = _webApplicationFactory.CreateScope())
      {
        var client = await _webApplicationFactory.CreateAuthenticatedClientForDefaultUserAsync(scope);
        var ef = _webApplicationFactory.GetContext(scope);
        // dados da requsição
        var jsonPayload = JsonConvert.SerializeObject(new
        {
          Title = "hello",
          StartDate = DateTime.Now,
          FinishDate = DateTime.Now.AddDays(3)
        });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        // realiza requisição
        var response = await client.PostAsync("/api/project-management/", content);
        response.EnsureSuccessStatusCode();
        // resultadp
        var result = await response.Content.ReadAsStringAsync();
        Assert.Equal("true", result);
        // pega direto do entity framework
        var projects = await ef.Projects.ToListAsync();
        Assert.Single(projects);
        // verifica se açao de criação de projeto foi persistida
        var actionTypeIds = ef.Actions.Select(a => a.ActionLogTypeId).ToList();
        Assert.Contains(ActionType.UserCreatedProject.Id, actionTypeIds);
      }
    }
    

  }
}