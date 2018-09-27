using System;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidRequestException : Exception
  {
    public InvalidRequestException() : base("bad request")
    {
    }

    public InvalidRequestException(string message) : base(message)
    {
    }
  }
}