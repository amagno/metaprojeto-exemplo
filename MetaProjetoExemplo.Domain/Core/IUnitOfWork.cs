using System.Threading;
using System.Threading.Tasks;

namespace MetaProjetoExemplo.Domain.Core
{
    public interface IUnitOfWork
    {
      Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}