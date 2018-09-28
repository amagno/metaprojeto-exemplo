using System;
using MediatR;

namespace MetaProjetoExemplo.Domain.Core
{
  public class AuthenticatedCommand<T, R> : IRequest<R> where T : IAuthenticatedRequest<R>
  {
    public T Command { get; private set; }
    public Guid UserIdentifier { get; private set; }
    public string IpAdress { get; private set; }
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