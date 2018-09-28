using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public interface IProjectManagerRepository : IRepository
  {
    ProjectManager Add(ProjectManager projectManager);
    ProjectManager Update(ProjectManager projectManager);
    Task<ProjectManager> GetByUserIdentifierAsync(Guid userIdentfier);
  }
}