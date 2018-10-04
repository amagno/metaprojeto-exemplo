using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MetaProjetoExemplo.Api;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MetaProjetoExemplo.FunctionalTests.ProjectManagement
{
  public class ProjectManagementWebApplication : WebApplicationFactory<Startup>, IDisposable
  {
    public User DefaultUser = new User("teste", "teste@teste.com", "123");
    private HttpClient _client;
    /// <summary>
    /// Cria cliente e retorna scopo
    /// </summary>
    /// <returns>Scopo</returns>
    public IServiceScope CreateScope()
    {
      _client = CreateClient();
      return Server.Host.Services.CreateScope();
    }
    /// <summary>
    /// Dispose pega contexto do banco de dados e deleta o mesmo
    /// </summary>
    /// <param name="disposing"></param>
    protected override void Dispose(bool disposing)
    {
      using (var scope = CreateScope())
      {
        var context = GetContext(scope);
        context.Database.EnsureDeleted();
        base.Dispose(disposing);
      }
    }
    /// <summary>
    /// Configura webapplication 
    /// </summary>
    /// <param name="builder"></param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      // var connection = new SqliteConnection("DataSource=:memory:");
      // connection.Open();

      builder.ConfigureServices(services => {
          services.AddDbContext<ExampleAppContext>(options => {
            options.UseSqlServer("Server=localhost;Integrated Security=false;Database=testing_example_app;User=sa;Password=abc123##");
            // options.UseSqlite("Data Source=testing.db");
          });

      });
    }
    /// <summary>
    /// Pega contexto do entity framework
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    public ExampleAppContext GetContext(IServiceScope scope)
    {
      return scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
    }
    /// <summary>
    /// Realiza migração do banco e adiciona o default user
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    private async Task SeedDataAsync(IServiceScope scope)
    {
      var ef = GetContext(scope);
      await ef.Database.EnsureCreatedAsync();
      ef.Users.Add(DefaultUser);
      await ef.SaveChangesAsync();
    }
    /// <summary>
    /// retorna cliente com headers de authorization
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    public async Task<HttpClient> CreateAuthenticatedClientForDefaultUserAsync(IServiceScope scope)
    {
      if (_client.DefaultRequestHeaders.Authorization != null)
      {
        return _client;
      }
      await SeedDataAsync(scope);
      var auth = scope.ServiceProvider.GetRequiredService<IAuthService>();
      var result =  await auth.LoginAsync(DefaultUser.Email, DefaultUser.Password);
      var authHeader = new AuthenticationHeaderValue("Bearer", result.Token);
      _client.DefaultRequestHeaders.Authorization = authHeader;
      return _client;
    }
  }
}