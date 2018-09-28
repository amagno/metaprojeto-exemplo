using System.Threading.Tasks;

namespace MetaProjetoExemplo.Application.Services.Common
{
  public interface IAuthService
  {
      /// <summary>
      /// Realiza login no sistema retornando um token valido
      /// </summary>
      /// <param name="email"></param>
      /// <param name="password"></param>
      /// <param name="ipAddress"></param>
      /// <returns>Jwt token</returns>
      Task<AuthData> LoginAsync(string email, string password);
  }
}