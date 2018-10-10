using MetaProjetoExemplo.Application.Exceptions;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidUserEmailException : InvalidRequestException
  {
    public InvalidUserEmailException(string email) : base(ErrorCode.InvalidUserEmail, $"Invalid user e-mail: {email}")
    {
    }
  }
}