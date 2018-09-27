using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MetaProjetoExemplo.Api;
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
      var factory = webApplicationFactory.WithWebHostBuilder(builder => {
        builder.ConfigureServices(services => {
          services.AddDbContext<ExampleAppContext>(options => {
            options.UseInMemoryDatabase("testing_login");
          });
        });
      });

     _webApplicationFactory = factory.Factories.FirstOrDefault() ?? factory;
    
    }
    /// <summary>
    /// Detalhe: para realizar migração no banco de dados na memoria as configuraçoes
    /// do entity framework nao pode ter funções ligadas ao banco de dados
    /// pois o banco de dados na memoria não possui funçoões por exemplo do SQLServer
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task TestName()
    {
      var client = _webApplicationFactory.CreateClient();

      var services = _webApplicationFactory.Server.Host.Services;

      using (var scope = services.CreateScope())
      {
        // serviço do entity framework
        var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();

        // realiza criação/migração do banco
        var create = await ef.Database.EnsureCreatedAsync();
        
        // verifica se o banco foi criado
        if (create)
        {
          var email = "teste@teste.com";
          var password = "123";
          var user = new User("teste", email, password);
          // adiciona usuario teste
          ef.Users.Add(user);
          await ef.SaveChangesAsync();

          // dados do login
          var jsonPayload = JsonConvert.SerializeObject(new {
            email,
            password
          });
          var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
          // realiza requisição
          var response = await client.PostAsync("/api/auth/login", content);
          // token
          var token = await response.Content.ReadAsStringAsync();
          // pega serviço de autenticação
          var jwt = scope.ServiceProvider.GetRequiredService<IJwtAuth>();
          // valida o token
          var result = jwt.ValidateToken(token);

          Assert.True(result.IsValid);
        } else {
          // FAIL
          Assert.True(false);
        }


        


      }    
    }



  }
}