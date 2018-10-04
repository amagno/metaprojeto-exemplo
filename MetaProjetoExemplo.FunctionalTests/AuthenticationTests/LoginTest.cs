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
    /// Detalhe: para realizar migração no banco de dados na memoria as configuraçoes
    /// do entity framework nao pode ter funções ligadas ao banco de dados
    /// pois o banco de dados na memoria não possui funçoões por exemplo do SQLServer
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Test_api_login_with_valid_user_should_response_valid_token()
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
        // novo usuario
        var email = "teste@teste.com";
        var password = "123";
        var user = new User("teste", email, password);
        // adiciona usuario teste e salva
        ef.Users.Add(user);
        await ef.SaveChangesAsync();
        // dados do login
        var jsonPayload = JsonConvert.SerializeObject(new
        {
          email,
          password
        });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        // realiza requisição
        var response = await client.PostAsync("/api/auth/login", content);
        response.EnsureSuccessStatusCode();
        // result
        var resultText = await response.Content.ReadAsStringAsync();

        var obj = JsonConvert.DeserializeObject<AuthData>(resultText);
        // pega serviço de autenticação
        var jwt = scope.ServiceProvider.GetRequiredService<IJwtAuth>();
        // valida o token
        var result = jwt.ValidateToken(obj.Token);
        Assert.True(result.IsValid);
      } //FIM DO SCOPO
      // verifica se eventos de acoes foram persitidos (tentativa de login), (sucesso no login)
      // deve renovar o scope para que o entity framework seja atualizado 
      // conforme persistencia
      using (var scope = services.CreateScope())
      {
        var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
        var actionTypeIds = ef.Actions.Select(a => a.ActionLogTypeId).ToList();

        Assert.Contains(ActionType.UserLoginAttempt.Id, actionTypeIds);
        Assert.Contains(ActionType.UserLoginSuccess.Id, actionTypeIds);
      }
    }
    [Fact]
    public async Task Test_api_login_with_invalid_user_password_should_response_bad_request()
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
        // novo usuario
        var email = "teste@teste.com";
        var password = "123";
        var user = new User("teste", email, password);
        // adiciona usuario teste e salva
        ef.Users.Add(user);
        await ef.SaveChangesAsync();
        // dados do login
        var jsonPayload = JsonConvert.SerializeObject(new
        {
          email,
          password = "WRONG_PASSWORD"
        });
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        // realiza requisição
        var response = await client.PostAsync("/api/auth/login", content);
        // result
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

      } //FIM DO SCOPO
      // verifica se eventos de acoes foram persitidos (tentativa de login), (falha no login)
      // deve renovar o scope para que o entity framework seja atualizado 
      // conforme persistencia
      using (var scope = services.CreateScope())
      {
        var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
        var actionTypeIds = ef.Actions.Select(a => a.ActionLogTypeId).ToList();

        Assert.Contains(ActionType.UserLoginAttempt.Id, actionTypeIds);
        Assert.Contains(ActionType.UserLoginFail.Id, actionTypeIds);
      }
    }
  }
}