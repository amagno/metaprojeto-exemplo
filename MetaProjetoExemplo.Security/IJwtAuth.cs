using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MetaProjetoExemplo.Security
{
  public interface IJwtAuth
  {
    string CreateToken(IEnumerable<Claim> claims, int expires = 600);
    JwtValidationResult ValidateToken(string jwt);
    JwtValidationResult ValidateHttpContext(HttpContext httpContext);
  }
}