

using System.ComponentModel.DataAnnotations;
using MediatR;
using MetaProjetoExemplo.Application.Services.Common;

namespace MetaProjetoExemplo.Application.Commands
{
  public class UserLoginCommand : IRequest<AuthData>
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    private string _ip;

    public void SetIp(string ip)
    {
      _ip = ip;
    }
    public string GetIp()
    {
      return _ip;
    }
  }
}