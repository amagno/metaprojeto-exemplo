using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.ProjectManagement;
using MetaProjetoExemplo.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MetaProjetoExemplo.Application.Queries
{
  public class ProjectManagerQueries : IProjectManagerQueries
  {
    private readonly IDbConnection _connection;
    public ProjectManagerQueries(IContextConnection contextConnection)
    {
      _connection = contextConnection.GetConnection();
    }

    public async Task<ProjectManagerViewModel> GetUserProjectManager(Guid identifier)
    {
      var data = await _connection.QueryAsync<dynamic>(@"
        select 
          pm.id as managerId,
          p.id as projectId,
          p.is_active,
          p.title,
          p.start_date,
          p.finish_date
        from project_management.project_managers as pm
        inner join project_management.projects as p on (p.project_manager_id = pm.id)
        where pm.user_identifier = @UserIdentifier
      ", new { UserIdentifier = identifier });

      return MapToProjectManagerViewModel(data);
    }
    private ProjectManagerViewModel MapToProjectManagerViewModel(dynamic data)
    {

      var viewModel = new ProjectManagerViewModel 
      {
        Id = data[0].managerId,
        Projects = new List<ProjectItem>()
      };

      foreach (var item in data)
      {
        viewModel.Projects.Add(new ProjectItem {
          Id = item.projectId,
          IsActive = item.is_active,
          Title = item.title,
          StartDate = item.start_date,
          FinishDate = item.finish_date
        });
      }
      return viewModel;
    }
  }
}