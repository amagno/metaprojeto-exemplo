using System;
using System.Collections.Generic;
using System.Linq;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public class ProjectManager : Entity
  {
    public Guid UserIdentifier { get; private set; }
    private readonly List<Project> _projects;
    // para navegação deve ser marcada como virtual
    public IReadOnlyCollection<Project> Projects => _projects;

    protected ProjectManager()
    {
      _projects = new List<Project>();
    }
    public ProjectManager(Guid userIdentifier) : this()
    {
      UserIdentifier = userIdentifier;
    }
    public int CountProjects()
    {
      return _projects.Count;
    }
    /// <summary>
    /// Adicionar projeto, o mesmo só pode ser adiconado caso a data de começo e fim
    /// não esteja dentro da data de começo e fim dos projetos já existentes
    /// </summary>
    /// <param name="title">titulo do projeto</param>
    /// <param name="startDate">data de começo</param>
    /// <param name="finishDate">data de termino</param>
    public Project AddProject(string title, DateTime startDate, DateTime finishDate)
    {
      if (_projects.Any(p => p.StartDate <= startDate && p.FinishDate >= startDate)) 
      {
        throw new InvalidProjectDateDomainException();
      }
      if (_projects.Any(p => p.StartDate <= finishDate && p.FinishDate >= finishDate)) 
      {
        throw new InvalidProjectDateDomainException();
      }
      var project = new Project(Id, title, startDate, finishDate);
      _projects.Add(project);
      return project;
    }
  }

}