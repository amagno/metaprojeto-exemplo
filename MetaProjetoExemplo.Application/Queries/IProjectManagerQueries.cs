using System;
using System.Threading.Tasks;

namespace MetaProjetoExemplo.Application.Queries
{
  public interface IProjectManagerQueries
  {
    Task<ProjectManagerViewModel> GetUserProjectManager(Guid identifier);
  }
}