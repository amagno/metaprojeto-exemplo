using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MetaProjetoExemplo.Api;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Application.ViewModels;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MetaProjetoExemplo.FunctionalTests.ProjectManagement
{
  public class ProjectManagementWebApplication : WebApplicationFactory<Startup>
  {
    public User DefaultUser = new User("teste", "teste@teste.com", "123");
    private HttpClient _client;
    public IServiceScope CreateScope()
    {
      _client = CreateClient();
      return Factories.FirstOrDefault() != null ? 
      Factories.First().Server.Host.Services.CreateScope() : 
      Server.Host.Services.CreateScope();
    }
    protected override void Dispose(bool disposing)
    {
      using (var scope = Server.Host.Services.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
        context.Database.EnsureDeleted();
        base.Dispose(disposing);
      }
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureServices(services => {
          services.AddDbContext<ExampleAppContext>(options => {
            options.UseInMemoryDatabase("testing_login");
          });
      });
    }
    public ExampleAppContext GetContext(IServiceScope scope)
    {
      return scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
    }
    private async Task SeedDataAsync(IServiceScope scope)
    {
      var ef = scope.ServiceProvider.GetRequiredService<ExampleAppContext>();
      var create = await ef.Database.EnsureCreatedAsync();
      if (!create)
      {
        throw new Exception("error on migrate database");
      }
      // adiciona usuario teste
      ef.Users.Add(DefaultUser);
      await ef.SaveChangesAsync();
    }
    public async Task<HttpClient> CreateAuthenticatedClientForDefaultUserAsync(IServiceScope scope)
    {
      if (_client.DefaultRequestHeaders.Authorization != null)
      {
        return _client;
      }
      await SeedDataAsync(scope);
      var auth = scope.ServiceProvider.GetRequiredService<IAuthService>();
      var token =  await auth.LoginAsync(DefaultUser.Email, DefaultUser.Password);
      var authHeader = new AuthenticationHeaderValue("Bearer", token);
      _client.DefaultRequestHeaders.Authorization = authHeader;
      return _client;
    }
  }
}