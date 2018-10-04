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
  public class FinalizeProjectTest : IClassFixture<ProjectManagementWebApplication>
  {
    private readonly ProjectManagementWebApplication _webApplicationFactory;
    public FinalizeProjectTest(ProjectManagementWebApplication webApplicationFactory)
    {
      _webApplicationFactory = webApplicationFactory;
    }
    /// <summary>
    /// Testa finalizar projeto, verifica se eventos de ação foram persistidos
    /// </summary>
    [Fact]
    public async Task Test_finalize_valid_project()
    {
      using (var scope = _webApplicationFactory.CreateScope())
      {
        var client = await _webApplicationFactory.CreateAuthenticatedClientForDefaultUserAsync(scope);
        var ef = _webApplicationFactory.GetContext(scope);
        // pega default user
        var user = await ef.Users.FirstOrDefaultAsync();
        // adiciona dados
        var projectManager = new ProjectManager(user.Identifier);
        projectManager.AddProject("teste", DateTime.Now, DateTime.Now.AddDays(2));
        ef.ProjectManagers.Add(projectManager);
        await ef.SaveChangesAsync();

        // pega projeto adicionado
        var project = projectManager.Projects.FirstOrDefault();

        // realiza requisição
        var response = await client.GetAsync($"/api/project-management/finalize/{project.Id}");
        response.EnsureSuccessStatusCode();
        // resposta
        var result = await response.Content.ReadAsStringAsync();
        Assert.Equal("true", result);
      }
      // cria um novo scopo para pegar o banco de dados 
      // do entity framework atualizado e verificar se eventos foram persistidos
      using (var scope = _webApplicationFactory.CreateScope())
      {
        var ef = _webApplicationFactory.GetContext(scope);
        var projectVerify = await ef.Projects.FirstOrDefaultAsync(p => p.Id == 1);
        var actionTypeIds = ef.Actions.Select(a => a.ActionLogTypeId).ToList();
        // verifica se o projeto esta marcado como não ativo
        Assert.False(projectVerify.IsActive);
        // verifica se o evento de acao projeto finalizado foi persistido
        Assert.Contains(ActionType.UserFinalizedProject.Id, actionTypeIds);
      }
    }
    

  }
}