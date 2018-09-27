using System.Security.Claims;

namespace MetaProjetoExemplo.Security
{
  public class JwtValidationResult
  {
    public ClaimsPrincipal Claims { get; private set; }
    public bool IsValid { get; private set; }
    public JwtValidationResult(ClaimsPrincipal claims, bool isValid)
    {
      Claims = claims;
      IsValid = isValid;
    }
  }
}