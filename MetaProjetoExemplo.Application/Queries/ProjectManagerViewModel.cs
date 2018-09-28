using System;
using System.Collections.Generic;

namespace MetaProjetoExemplo.Application.Queries
{
  public class ProjectManagerViewModel
  {
    public int Id { get; set; }
    public IEnumerable<ProjectItem> Projects { get; set; }
  }
  public class ProjectItem
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime FinishDate { get; set; }
  }
}