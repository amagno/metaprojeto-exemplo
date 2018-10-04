using System.Data;

namespace MetaProjetoExemplo.Domain.Core
{
  public interface IContextConnection
  {
    IDbConnection GetConnection();
  }
}