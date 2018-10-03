using System;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidProjectManagerException : UnauthorizedException
  {
    public InvalidProjectManagerException(string message) : base($"invalid project manager userId: {message}")
    {}

    public InvalidProjectManagerException(Guid userId) : this(userId.ToString())
    {}
  }
}