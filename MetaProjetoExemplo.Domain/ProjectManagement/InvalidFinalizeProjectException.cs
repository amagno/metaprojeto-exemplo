using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public class InvalidFinalizeProjectException : DomainException
  {
    public InvalidFinalizeProjectException() : base("invalid date to finalize project")
    {
    }
  }
}