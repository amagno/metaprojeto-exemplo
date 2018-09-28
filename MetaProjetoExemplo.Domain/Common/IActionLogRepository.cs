using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
  public interface IActionRepository : IRepository
  {
    Action Add(Action action);
  }
}