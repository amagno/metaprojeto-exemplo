using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MetaProjetoExemplo.Domain.Core
{
    public interface IUnitOfWork
    {
      Task<bool> CommitAsync();
      IDbConnection GetConnection();
    }
}