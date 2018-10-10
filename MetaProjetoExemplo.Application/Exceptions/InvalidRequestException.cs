using System;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public enum ErrorCode 
  {
    NoErrorCode = 0,
    InvalidUserEmail = 1,
    InvalidUserIdentifier = 2,
    InvalidUserPassword = 3,
    InvalidProjectId = 4,
    InvalidProjectDate = 5
  }
  public class InvalidRequestException : Exception
  {
    /// <summary>
    /// Codigo de erro
    /// </summary>
    /// <value></value>
    public ErrorCode ErrorCode { get; private set; }
    public InvalidRequestException(ErrorCode code) : base("bad request")
    {
      ErrorCode = code;
    }

    public InvalidRequestException(ErrorCode code, string message) : base(message)
    {
      ErrorCode = code;
    }
  }
}