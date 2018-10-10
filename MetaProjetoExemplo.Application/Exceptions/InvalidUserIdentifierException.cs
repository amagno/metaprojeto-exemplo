using System;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidUserIdentifierException : InvalidRequestException
  {
    public InvalidUserIdentifierException(Guid identifier) : 
      base(ErrorCode.InvalidUserIdentifier, $"Invalid user identifier: {identifier.ToString()}")
    {
    }
  }
}