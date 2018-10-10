namespace MetaProjetoExemplo.Application.Exceptions
{
  public class InvalidProjectIdException : InvalidRequestException
  {
    public InvalidProjectIdException(int id) : base(ErrorCode.InvalidProjectId, $"project id is invalid: {id}")
    {
    }
  }
}