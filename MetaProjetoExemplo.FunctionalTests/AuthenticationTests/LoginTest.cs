using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MetaProjetoExemplo.Api;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Infrastructure;
using MetaProjetoExemplo.Security;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace MetaProjetoExemplo.FunctionalTests.AuthenticationTests
{
  public class LoginTest : IClassFixture<WebApplicationFactory<Startup>>
  {
    private readonly WebApplicationFactory<Startup> _webApplicationFactory;
    private User _user => new User("teste", "teste@teste.com", "123");
    public LoginTest(WebApplicationFactory<Startup> webApplicationFactory)
    {
      // cria web application com bando de dados na memoria
      _webApplicationFactory = webApplicationFactory.WithWebHostBuilder(builder =>
      {
        builder.ConfigureServices(services =>
        {
          services.AddDbContext<ExampleAppContext>(options =>
          {
            options.UseInMemoryDatabase("testing_login");
          });
        });
      });
    }
    /// <summary>
    /// Testa a funçao de login da api respondendo um token valido
    /// verifica se eventos de ação foram persistidos
    /// 
    /// Detalhe: para realizar migração no banco de dados na memoria as configuraçoes
    /// do entity framework nao pode ter funções ligadas ao banco de dados
    /// pois o banco de dados na memoria não possui funçoões por exemplo do SQLServer
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_api_login_with_valid_user_should_response_valid_token()
    {
      var client = await CreateClientAsync();
      // dados do login
      var jsonPayload = JsonConvert.SerializeObject(new
      {
        _user.Email,
        _user.Password
      });
      var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
      // realiza requisição
      var response = await client.PostAsync("/api/auth/login", content);
      response.EnsureSuccessStatusCode();
      // result
      var resultText = await response.Content.ReadAsStringAsync();
      var obj = JsonConvert.DeserializeObject<AuthData>(resultText);
      
      // verifica se eventos de acoes foram persitidos (tentativa de login), (sucesso no login)
      // deve renovar o scope para que o entity framework seja atualizado 
      // conforme persistencia
      using (var scope = CreateScope())
      {
        // pega serviço de autenticação
        var jwt = scope.ServiceProvider.GetRequiredService<IJwtAuth>();
        // valida o token
        var result = jwt.ValidateToken(obj.Token);
        Assert.True(result.IsValid);
        // serviço do entity framework
        var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
        var actionTypeIds = ef.Actions.Select(a => a.ActionLogTypeId).ToList();
        // verifica se eventos de ação foram persistidos
        Assert.Contains(ActionType.UserLoginAttempt.Id, actionTypeIds);
        Assert.Contains(ActionType.UserLoginSuccess.Id, actionTypeIds);
      }
    }
    [Fact]
    public async Task Test_api_login_with_invalid_user_password_should_response_bad_request()
    {
      var client = await CreateClientAsync();
      var jsonPayload = JsonConvert.SerializeObject(new
      {
        _user.Email,
        password = "WRONG_PASSWORD"
      });
      var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
      // realiza requisição
      var response = await client.PostAsync("/api/auth/login", content);
      // result
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
      // verifica se eventos de acoes foram persitidos (tentativa de login), (falha no login)
      // deve renovar o scope para que o entity framework seja atualizado 
      // conforme persistencia
      using (var scope = CreateScope())
      {
        // serviço do entity framework
        var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
        var actionTypeIds = ef.Actions.Select(a => a.ActionLogTypeId).ToList();
        // verifica se eventos de ação foram persistidos
        Assert.Contains(ActionType.UserLoginAttempt.Id, actionTypeIds);
        Assert.Contains(ActionType.UserLoginFail.Id, actionTypeIds);
      }
    }
    private IServiceScope CreateScope()
    {
     return _webApplicationFactory.Server.Host.Services.CreateScope();
    }
    private async Task<HttpClient> CreateClientAsync()
    {
      var client = _webApplicationFactory.CreateClient();
      var services = _webApplicationFactory.Server.Host.Services;
      // cria scopo de servicos
      using (var scope = services.CreateScope())
      {
        // serviço do entity framework
        var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
        // realiza criação/migração do banco
        await ef.Database.EnsureCreatedAsync();
        // adiciona usuario teste e salva
        ef.Users.Add(_user);
        await ef.SaveChangesAsync();
        return client;
      }
    }
  }
}