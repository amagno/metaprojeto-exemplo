using System;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.ProjectManagement
{
  public class Project : Entity
  {
    int? _projectManagerId;
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
    /// <summary>
    /// Novo projeto
    /// Regras: a data final do projeto deve ser pelo menos um dia superior
    /// a data inicial
    /// </summary>
    /// <param name="managerId">Id do gerente de projeto</param>
    /// <param name="title">Titulo do projeto</param>
    /// <param name="startDate">Data de começo do projeto</param>
    /// <param name="finishDate">Data final do projeto</param>
    /// <returns></returns>
    public Project(int projectManagerId, string title, DateTime startDate, DateTime finishDate) : this()
    {
      if (finishDate <= startDate)
      {
        throw new InvalidProjectDateDomainException();
      }
      _projectManagerId = projectManagerId;
      Title = title;
      StartDate = startDate;
      FinishDate = finishDate;
    }
    /// <summary>
    /// Finalizar projeto.
    /// </summary>
    public void FinalizeNow()
    {
      FinishDate = DateTime.Now; 
      IsActive = false;
    }
  }
}