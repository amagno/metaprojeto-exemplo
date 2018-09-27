using System;

namespace MetaProjetoExemplo.Domain.Core
{
  public class DomainException : Exception
  {
    public DomainException()
    {
    }
    public DomainException(string message) : base(message)
    {
    }
  }
}