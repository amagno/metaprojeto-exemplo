using System;
using System.Linq;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;

namespace MetaProjetoExemplo.Infrastructure.Repositories.ProjectManagement
{
  public class ProjectManagerRepository : EntityRepository<ProjectManager>, IProjectManagerRepository
  {
    public ProjectManagerRepository(ExampleAppContext dbContext) : base(dbContext)
    {
    }

    public ProjectManager Add(ProjectManager projectManager)
    {
      return _entity.Add(projectManager).Entity;
    }

    public Task<ProjectManager> GetByUserIdentifierAsync(Guid userIdentfier)
    {
      return _entity
        .Include(p => p.Projects)
        .FirstOrDefaultAsync(p => p.UserIdentifier == userIdentfier);
    }

    public void Update(ProjectManager projectManager)
    {
      _entity.Update(projectManager);
    }
  }
}