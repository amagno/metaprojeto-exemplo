using System;
using System.Collections.Generic;
using System.Security.Claims;
using MetaProjetoExemplo.Security;
using Xunit;

namespace tests.adventech_api.Helpers
{
  public class JwtAuthTests
  {
    // CLAIM TYPES
    const string USER_ID = "user_id";
    const string AUDIENCE = "aud";
    const string ISSUER = "iss";

    [Fact]
    public void TestCreateAndValidateToken()
    {
      var secret = "2Fkc2Fkc2FkYXNkMTIzMTJhc2Rhc2RzYWRhc2Q=232";
      var aud = "teste";
      var issuer = "teste";
      var jwtAuth = new JwtAuth(secret, aud, issuer);
      var userId = 1;
      var claims = CreateClaims(userId);
      var token = jwtAuth.CreateToken(claims);
      var validation = jwtAuth.ValidateToken(token);

      if (validation.IsValid) {
        var userIdClaim = validation.Claims.FindFirst(c => c.Type == USER_ID).Value;
        Assert.Equal(userId.ToString(), userIdClaim);
      } else {
        Assert.True(false);
      }
      
    }

    [Fact]
    public void TestCreateAndValidateInvalidToken()
    {
      var secret = "2Fkc2Fkc2FkYXNkMTIzMTJhc2Rhc2RzYWRhc2Q=232";
      var aud = "teste";
      var issuer = "teste";
      var secretInvalid = "2Fkc2Fkc2FkYXNkMTIzMTJhc2Rhc2RzYWRhc2Q=";
      var audInvalid = "teste";
      var issuerInvalid = "teste";
      var jwtAuth = new JwtAuth(secret, aud, issuer);
      var jwtAuthIvalid = new JwtAuth(secretInvalid, audInvalid, issuerInvalid);
      var userId = 1;

      var claims = CreateClaims(userId);

      var token = jwtAuth.CreateToken(claims);
      var validation = jwtAuthIvalid.ValidateToken(token);

      Assert.False(validation.IsValid);
      Assert.Null(validation.Claims);
    }
    private IEnumerable<Claim> CreateClaims(int userId)
    {
      return new []
      {
        new Claim(USER_ID, Convert.ToString(userId)),
      };
    }
  }
}