using System;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public class Project : Entity
  {
    private int? _managerId;
    // Apenas para navegação
    public ProjectManager Manager { get; private set; }
    public string Title { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime FinishDate { get; private set; }
    public bool IsActive { get; private set; }
    protected Project()
    {
      CreatedDate = DateTime.Now;
      IsActive = true;
    }
    public Project(int managerId, string title, DateTime startDate, DateTime finishDate) : this()
    {
      if (finishDate <= startDate)
      {
        throw new InvalidProjectDateException();
      }
      _managerId = managerId;
      Title = title;
      StartDate = startDate;
      FinishDate = finishDate;
    }
    /// <summary>
    /// Finalizar projeto, so pode finalizar caso a data de termino do projeto 
    /// tenha passado.
    /// </summary>
    public void FinalizeThis()
    {
      if (FinishDate > DateTime.Now)
      {
        throw new InvalidFinalizeProjectException();
      }
      
      IsActive = false;
    }
  }
}