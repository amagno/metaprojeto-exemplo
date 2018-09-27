using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
  public interface IActionLogRepository : IRepository
  {
    ActionLog Add(ActionLog log);
  }
}