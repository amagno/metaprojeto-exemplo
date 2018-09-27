using MetaProjetoExemplo.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace MetaProjetoExemplo.Infrastructure.Repositories
{
  public abstract class EntityRepository<T> : IRepository where T : Entity
  {
    private readonly ExampleAppContext _dbContext;
    protected DbSet<T> _entity => _dbContext.Set<T>();
    public IUnitOfWork UnitOfWork => _dbContext;

    public EntityRepository(ExampleAppContext dbContext)
    {
      _dbContext = dbContext;
    }

  }
}