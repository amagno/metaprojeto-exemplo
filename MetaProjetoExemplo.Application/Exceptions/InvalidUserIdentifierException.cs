using System;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidUserIdentifierException : InvalidRequestException
  {
    public InvalidUserIdentifierException(Guid identifier) : 
      base($"Invalid user identifier: {identifier.ToString()}")
    {
    }
  }
}