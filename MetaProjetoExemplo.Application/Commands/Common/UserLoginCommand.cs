

using System.ComponentModel.DataAnnotations;
using MediatR;
using MetaProjetoExemplo.Application.Services.Common;

namespace MetaProjetoExemplo.Application.Commands.Common
{
  public class UserLoginCommand : IRequest<AuthData>
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string IpInfo { get;  set; }

    public UserLoginCommand(string email, string password, string ipInfo)
    {
      Email = email;
      Password = password;
      IpInfo = ipInfo;
    }

  }
}