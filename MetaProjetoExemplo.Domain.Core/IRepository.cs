namespace MetaProjetoExemplo.Domain.Core
{
  public interface IRepository
  {
    IUnitOfWork UnitOfWork { get; }
  }
}