using System;
using System.Security.Claims;
using MetaProjetoExemplo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MetaProjetoExemplo.Application.Extesions
{
  public static class HttpAuthExtesion
  {
    public static Guid GetUserIdentifier(this HttpContext context)
    {
      // Pega o servico JwtAuth
      var jwtAuth = context.RequestServices.GetService<IJwtAuth>();
      // Valida com serviço
      var validation = jwtAuth.ValidateHttpContext(context);
      // get user idetifier claim
      var stringIdentifier = validation.Claims.FindFirst(ClaimTypes.Sid).Value;
      // try parse
      Guid identifier;
      if (Guid.TryParse(stringIdentifier, out identifier))
      {
        return identifier;
      }

      throw new Exception("user identifier is not guid");
    }
  }
}