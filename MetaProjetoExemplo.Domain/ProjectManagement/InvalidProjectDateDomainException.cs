using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public class InvalidProjectDateDomainException : DomainException
  {
    public InvalidProjectDateDomainException() : base("Invalid project date")
    {
    }

    public InvalidProjectDateDomainException(string message) : base(message)
    {
    }
  }
}