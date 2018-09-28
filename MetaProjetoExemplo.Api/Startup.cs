using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Queries;
using MetaProjetoExemplo.Application.Services;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.ProjectManagement;
using MetaProjetoExemplo.Infrastructure.Repositories.Common;
using MetaProjetoExemplo.Infrastructure.Repositories.ProjectManagement;
using MetaProjetoExemplo.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace MetaProjetoExemplo.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddScoped<IJwtAuth, JwtAuth>(sp =>
      {
        return new JwtAuth(
          Configuration["Jwt:Secret"],
          Configuration["Jwt:Audience"],
          Configuration["Jwt:Issuer"]
        );
      });


      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IActionRepository, ActionRepository>();
      services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();

      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IProjectManagerQueries, ProjectManagerQueries>();

      services.AddMediatR();

      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new Info { Title = "Example App Api", Version = "v1" });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example App Api V1");
          c.RoutePrefix = string.Empty;
        });
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
