

using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public class CreateProjectCommand : ProjectCommandBase, IRequest<bool>
  {
    // request data properties
    [Required]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime FinishDate { get; set; }
   
    public CreateProjectCommand(string title, DateTime startDate, DateTime finishDate, Guid userIdentifier, string ipInfo) : base(userIdentifier, ipInfo)
    {
      Title = title;
      StartDate = startDate;
      FinishDate = finishDate;      
    }
  }
}