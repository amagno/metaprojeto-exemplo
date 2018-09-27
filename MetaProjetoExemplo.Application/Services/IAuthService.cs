using System.Threading.Tasks;
using MetaProjetoExemplo.Application.ViewModels;

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
      Task<string> LoginAsync(string email, string password);
  }
}