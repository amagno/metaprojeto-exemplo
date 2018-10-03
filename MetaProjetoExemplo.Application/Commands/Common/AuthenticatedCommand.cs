using System;
using MediatR;

namespace MetaProjetoExemplo.Application.Commands.Common
{
  public class AuthenticatedCommand<T, R> : IRequest<R> where T : IRequest<R>
  {
    public T Command { get; private set; }
    public Guid UserIdentifier { get; private set; }
    public string IpAdress { get; private set; }
    /// <summary>
    /// Encapsula um commando para adiciona UserIdentifier
    /// do usuario authenticado
    /// </summary>
    /// <param name="command">Commando a ser processado</param>
    /// <param name="userIdentifier">Guid do usuario logado</param>
    public AuthenticatedCommand(T command, Guid userIdentifier)
    {
      Command = command;
      UserIdentifier = userIdentifier;
    }
    public AuthenticatedCommand(T command, Guid userIdentifier, string ip) : this(command, userIdentifier)
    {
      IpAdress = ip;
    }
  }
}