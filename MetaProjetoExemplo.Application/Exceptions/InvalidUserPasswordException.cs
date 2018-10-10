using MetaProjetoExemplo.Application.Exceptions;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidUserPasswordException : InvalidRequestException
  {
    public InvalidUserPasswordException(string message = null) : base(ErrorCode.InvalidUserPassword, "Invalid password " + message)
    {
    }
  }
}