using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetaProjetoExemplo.Application.ViewModels
{
  public class ProjectBase
  {
    [Required]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime FinishDate { get; set; }

  }
  public class NewProject : ProjectBase
  {}
  public class ProjectCreated
  {
    public int Id { get; set; }
    public bool Success { get; set; }
  }
  public class ProjectManagerItem
  {
    public int Id { get; set; }
    public IEnumerable<ProjectItem> Projects { get; set; }
  }
  public class ProjectItem : ProjectBase
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}