using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.ViewModels;

namespace MetaProjetoExemplo.Application.Services
{
  public interface IProjectManagementService
  {
    Task<ProjectCreated> CreateProject(Guid userIdentifier, NewProject newProjectData);
    Task<ProjectManagerItem> GetUserProjects(Guid userIdentifier); 
  }
}