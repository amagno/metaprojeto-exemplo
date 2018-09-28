

using System;
using System.ComponentModel.DataAnnotations;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Application.Commands
{
  public class CreateProjectCommand : IAuthenticatedRequest<int>
  {
    // request data properties
    [Required]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime FinishDate { get; set; }

    public CreateProjectCommand(string title, DateTime startDate, DateTime finishDate)
    {
      Title = title;
      StartDate = startDate;
      FinishDate = finishDate;
    }
  }
}