using MetaProjetoExemplo.Domain.Common;

namespace MetaProjetoExemplo.Infrastructure.Repositories.Common
{
  public class ActionRepository : EntityRepository<Action>, IActionRepository
  {
    public ActionRepository(ExampleAppContext dbContext) : base(dbContext)
    {
    }

    public Action Add(Action action)
    {
      return _entity.Add(action).Entity;
    }
  }
}