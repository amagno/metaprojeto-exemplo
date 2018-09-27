

using System;
using MetaProjetoExemplo.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MetaProjetoExemplo.Application.Attributes
{
  public class JwtAuthorizeAttribute : Attribute, IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context) {}

    public void OnActionExecuting(ActionExecutingContext context)
    {
      // Pega o servico JwtAuth
      var jwtAuth = context.HttpContext.RequestServices.GetService<IJwtAuth>();
      // Valida com serviço
      var validation = jwtAuth.ValidateHttpContext(context.HttpContext);
      //  Valida o token
      if (!validation.IsValid) 
      {
        context.Result = new UnauthorizedResult();
        return;
      }
    }
    
  }
}