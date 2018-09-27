using MetaProjetoExemplo.Domain.Common;

namespace MetaProjetoExemplo.Infrastructure.Repositories.Common
{
  public class ActionLogRepository : EntityRepository<ActionLog>, IActionLogRepository
  {
    public ActionLogRepository(ExampleAppContext dbContext) : base(dbContext)
    {
    }

    public ActionLog Add(ActionLog log)
    {
      return _entity.Add(log).Entity;
    }
  }
}