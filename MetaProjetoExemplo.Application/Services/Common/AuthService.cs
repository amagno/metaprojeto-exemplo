using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.ViewModels;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Security;
using Microsoft.AspNetCore.Http;

namespace MetaProjetoExemplo.Application.Services.Common
{
  public class AuthService : IAuthService
  {
    private readonly IUserRepository _userRepository;
    private readonly IActionLogService _actionLogService;
    private readonly IJwtAuth _jwt;
    public AuthService(
      IUserRepository userRepository, IActionLogService actionLogService, IJwtAuth jwt
      )
    {
      _actionLogService = actionLogService;
      _userRepository = userRepository;
      _jwt = jwt;
    }
    /// <summary>
    /// Login do usuário do dominio
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="ipAddress"></param>
    /// <returns>Token de autenticação</returns>
    public async Task<string> LoginAsync(Login loginData, string ipAddress = null)
    {
      var user = await _userRepository.GetByEmailAsync(loginData.Email);
      await _actionLogService.RegisterLogAsync(ActionLogType.UserLoginAttempt, ipAddress);

      if (user == null)
      {
        await _actionLogService.RegisterLogAsync(ActionLogType.UserLoginFail, ipAddress);
        throw new InvalidUserEmailException(loginData.Email);
      }

      if (!ValidateUserPassword(user, loginData.Password))
      {
        await _actionLogService.RegisterLogAsync(ActionLogType.UserLoginFail, ipAddress);
        throw new InvalidUserPasswordException();
      }

      await _actionLogService.RegisterLogAsync(ActionLogType.UserLoginSuccess, user.Identifier, ipAddress);
      return _jwt.CreateToken(CreateClaims(user));

    }
    /// <summary>
    /// Cria claims de autenticação para o token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private IEnumerable<Claim> CreateClaims(User user)
    {
      return new List<Claim> {
        new Claim(ClaimTypes.Sid, user.Identifier.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
      };
    }
    /// <summary>
    /// Valida senha do usuário do dominio
    /// Sem criptografia!!!!
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private bool ValidateUserPassword(User user, string password)
    {
      return user.Password == password;
    }
  }
}