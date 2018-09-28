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
  public class CreateProjectTest : IClassFixture<ProjectManagementWebApplication>
  {
    private readonly ProjectManagementWebApplication _webApplicationFactory;
    public CreateProjectTest(ProjectManagementWebApplication webApplicationFactory)
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

        var id = int.Parse(result);

        Assert.NotEqual(0, id);
        // pega direto do entity framework
        var projects = await ef.Projects.ToListAsync();
        Assert.Single(projects);
      }
    }
    

  }
}