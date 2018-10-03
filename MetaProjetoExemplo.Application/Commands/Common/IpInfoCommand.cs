using MediatR;

namespace MetaProjetoExemplo.Application.Commands.Common
{
  /// <summary>
  /// Encapsula um commando para que seja informado o ip
  /// </summary>
  /// <typeparam name="T">Tipo do commando</typeparam>
  /// <typeparam name="R">Tipo de retorno do commando</typeparam>
  public class IpInfoCommand<T, R> : IRequest<R> where T : IRequest<R>
  {
    public T Command { get; private set; }
    public string IpInfo { get; private set; }
    public IpInfoCommand(T command, string ip)
    {
      Command = command;
      IpInfo = string.IsNullOrEmpty(ip) ? "no_ip_info" : ip;
    }

  }
}