using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.ViewModels;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.ProjectManagement;

namespace MetaProjetoExemplo.Application.Services
{
  public class ProjectManagementService : IProjectManagementService
  {
    private readonly IProjectManagerRepository _projectMangerRepository;
    public ProjectManagementService(IUserRepository userRepository, IProjectManagerRepository projectManagerRepository)
    {
      _projectMangerRepository = projectManagerRepository;
    }
    public async Task<ProjectCreated> CreateProject(Guid userIdentifier, NewProject newProjectData)
    {
      try
      {
        var project = await GetOrCreateProjectManagerAndAddProject(userIdentifier, newProjectData);
        await _projectMangerRepository.UnitOfWork.SaveChangesAsync();
        return new ProjectCreated {
          Id = project.Id,
          Success = true
        };
      } 
      catch (DomainException e)
      {
        throw new InvalidRequestException(e.Message);
      }
    }

    public async Task<ProjectManagerItem> GetUserProjects(Guid userIdentifier)
    {
      var projectManager = await _projectMangerRepository.GetByUserIdentifierAsync(userIdentifier);

      return projectManager == null ? null :
        new ProjectManagerItem {
          Id = projectManager.Id,
          Projects = projectManager.Projects == null ? 
            new List<ProjectItem>() : 
            projectManager.Projects.Select(p => new ProjectItem {
              Id = p.Id,
              Title = p.Title,
              StartDate = p.StartDate,
              FinishDate = p.FinishDate,
              CreatedDate =  p.CreatedDate
            }).ToList()
        };
    }

    private async Task<Project> GetOrCreateProjectManagerAndAddProject(Guid userIdentifier, NewProject newProjectData)
    {
      var exists = await _projectMangerRepository.GetByUserIdentifierAsync(userIdentifier);
      Project project;
      if (exists == null)
      {
        var projectManager = new ProjectManager(userIdentifier);
        project = projectManager.AddProject(newProjectData.Title, newProjectData.StartDate, newProjectData.FinishDate);
        _projectMangerRepository.Add(projectManager);
        return project;
      }

      project = exists.AddProject(newProjectData.Title, newProjectData.StartDate, newProjectData.FinishDate);
      _projectMangerRepository.Update(exists);
      return project;
    }
  }
}