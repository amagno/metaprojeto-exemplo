using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public class InvalidProjectDateException : DomainException
  {
    public InvalidProjectDateException() : base("Invalid project date")
    {
    }

    public InvalidProjectDateException(string message) : base(message)
    {
    }
  }
}