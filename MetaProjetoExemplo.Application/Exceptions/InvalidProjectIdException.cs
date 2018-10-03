namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidProjectIdException : InvalidRequestException
  {
    public InvalidProjectIdException(int id) : base($"project id is invalid: {id}")
    {
    }
  }
}