using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MetaProjetoExemplo.Security
{
  public class JwtAuth : IJwtAuth
  {
    private readonly SecurityKey _signInKey;
    private readonly string _audience;
    private readonly string _issuer;

    public JwtAuth(string secret, string audience, string issuer)
    {

      if (string.IsNullOrEmpty(secret))throw new ArgumentException(nameof(secret));
      if (string.IsNullOrEmpty(audience))throw new ArgumentException(nameof(audience));
      if (string.IsNullOrEmpty(issuer))throw new ArgumentException(nameof(issuer));

      var encoded = System.Text.Encoding.UTF8.GetBytes(secret);
      _signInKey = new SymmetricSecurityKey(encoded);
      _audience = audience;
      _issuer = issuer;

    }
    public JwtValidationResult ValidateToken(string jwt)
    {
      var validationParameters = new TokenValidationParameters
      {
        // Clock skew compensates for server time drift.
        // We recommend 5 minutes or less:
        ClockSkew = TimeSpan.FromMinutes(5),
        // Specify the key used to sign the token:
        IssuerSigningKey = _signInKey,
        RequireSignedTokens = true,
        // Ensure the token hasn't expired:
        RequireExpirationTime = true,
        ValidateLifetime = true,
        // Ensure the token audience matches our audience value (default true):
        ValidateAudience = true,
        ValidAudience = _audience,
        // Ensure the token was issued by a trusted authorization server (default true):
        ValidateIssuer = true,
        ValidIssuer = _issuer
      };

      try
      {
        var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out var rawValidatedToken);
        return new JwtValidationResult(claimsPrincipal, rawValidatedToken != null);
      }
      catch (Exception)
      {
        return new JwtValidationResult(null, false);
      }

    }
    public string CreateToken(IEnumerable<Claim> claims, int expires = 600)
    {
      var expireMinutes = DateTime.Now.AddMinutes(Convert.ToDouble(expires));
      var credentials = new SigningCredentials(_signInKey, SecurityAlgorithms.HmacSha256Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Expires = expireMinutes,
        SigningCredentials = credentials,
        Subject = new ClaimsIdentity(claims),
        Issuer = _issuer,
        Audience = _audience,
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
    public JwtValidationResult ValidateHttpContext(HttpContext httpContext)
    {
      var headers = httpContext.Request.Headers;

      if (!headers.ContainsKey("Authorization"))
      {
        return new JwtValidationResult(null, false);
      }
      var bearer = Convert.ToString(headers["Authorization"]).Trim();

      if (!bearer.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) || bearer.Length < 15)
      {
        return new JwtValidationResult(null, false);
      }
      
      var subtoken = bearer.Substring(7);

      try 
      {
        var token = JsonConvert.DeserializeObject<string>(subtoken);
        return ValidateToken(token);
      }
      catch (Exception)
      {
        return ValidateToken(subtoken);
      }
    }
  }
}