using System;
using System.Linq;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.ProjectManagement;
using MetaProjetoExemplo.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MetaProjetoExemplo.Application.Queries
{
  public class ProjectManagerQueries : IProjectManagerQueries
  {
    private readonly ExampleAppContext _context;
    public ProjectManagerQueries(ExampleAppContext context)
    {
      _context = context;
    }

    public async Task<ProjectManagerViewModel> GetUserProjectManager(Guid identifier)
    {
      var query = await _context
        .ProjectManagers
        .Include(pm => pm.Projects)
        .FirstOrDefaultAsync(pm => pm.UserIdentifier == identifier);

      return MapToProjectManagerViewModel(query);
    }
    private ProjectManagerViewModel MapToProjectManagerViewModel(ProjectManager projectManager)
    {
      return new ProjectManagerViewModel 
      {
        Id = projectManager.Id,
        Projects = projectManager.Projects.Select(p => new ProjectItem 
        {
          Id = p.Id,
          Title = p.Title,
          StartDate = p.StartDate,
          FinishDate = p.FinishDate
        })
      };
    }
  }
}