using MetaProjetoExemplo.Application.Exceptions;

namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidUserEmailException : InvalidRequestException
  {
    public InvalidUserEmailException(string email) : base($"Invalid user e-mail: {email}")
    {
    }
  }
}