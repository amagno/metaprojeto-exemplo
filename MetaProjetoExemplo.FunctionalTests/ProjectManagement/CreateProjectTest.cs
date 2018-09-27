using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MetaProjetoExemplo.Api;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Application.ViewModels;
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
      // var factory = webApplicationFactory.WithWebHostBuilder(builder => {
      //   builder.ConfigureServices(services => {
      //     services.AddDbContext<ExampleAppContext>(options => {
      //       options.UseInMemoryDatabase("testing_login");
      //     });
      //   });
      // });

      // _webApplicationFactory = factory.Factories.FirstOrDefault() ?? factory;

      _webApplicationFactory = webApplicationFactory;

    }
    // /// <summary>
    // /// Detalhe: para realizar migração no banco de dados na memoria as configuraçoes
    // /// do entity framework nao pode ter funções ligadas ao banco de dados
    // /// pois o banco de dados na memoria não possui funçoões por exemplo do SQLServer
    // /// </summary>
    // /// <returns></returns>
    // [Fact]
    // public async Task Test_create_project_with_valid_data()
    // {
    //   var client = _webApplicationFactory.CreateClient();

    //   var services = _webApplicationFactory.Server.Host.Services;

    //   using (var scope = services.CreateScope())
    //   {
    //     // serviço do entity framework
    //     var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();

    //     // realiza criação/migração do banco
    //     var create = await ef.Database.EnsureCreatedAsync();

    //     // verifica se o banco foi criado
    //     if (create)
    //     {
    //       var email = "teste@teste.com";
    //       var password = "123";
    //       var user = new User("teste", email, password);
    //       // adiciona usuario teste
    //       ef.Users.Add(user);
    //       await ef.SaveChangesAsync();
    //       // gera token de autenticação
    //       var auth = scope.ServiceProvider.GetRequiredService<IAuthService>();
    //       var token =  await auth.LoginAsync(new Login {
    //         Email = email,
    //         Password = password
    //       });
    //       var authHeader = new AuthenticationHeaderValue("Bearer", token);
    //       client.DefaultRequestHeaders.Authorization = authHeader;
    //       // dados da requsição
    //       var jsonPayload = JsonConvert.SerializeObject(new NewProject {
    //         Title = "hello",
    //         StartDate = DateTime.Now,
    //         FinishDate = DateTime.Now.AddDays(3)
    //       });
    //       var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
    //       // realiza requisição
    //       var response = await client.PostAsync("/api/project-management/", content);
    //       // resultadp
    //       var result = await response.Content.ReadAsStringAsync();
    //       var objectResult = JsonConvert.DeserializeObject<ProjectCreated>(result);

    //       Assert.Equal(1, objectResult.Id);
    //       Assert.True(objectResult.Success);
    //       // pega direto do entity framework
    //       var projects = await ef.Projects.ToListAsync();
    //       Assert.Single(projects);
    //     } else {
    //       // FAIL
    //       Assert.True(false);
    //     }

    //   }    
    // }
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
        var jsonPayload = JsonConvert.SerializeObject(new NewProject
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

        var objectResult = JsonConvert.DeserializeObject<ProjectCreated>(result);

        Assert.NotEqual(0, objectResult.Id);
        Assert.True(objectResult.Success);
        // pega direto do entity framework
        var projects = await ef.Projects.ToListAsync();
        Assert.Single(projects);
      }
    }
    

  }
}