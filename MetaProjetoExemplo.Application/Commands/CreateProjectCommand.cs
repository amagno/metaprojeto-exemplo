using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MetaProjetoExemplo.Application.Exceptions;

namespace MetaProjetoExemplo.Application.Commands
{
  public class CreateProjectCommand : IRequest<int>
  {
    // request data properties
    [Required]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime FinishDate { get; set; }

    public Guid UserIdentifier { get; private set; }
    // 
    private Guid _uid;
    public void SetUserIdentifier(Guid uid)
    {
      _uid = uid;
    }
    public Guid GetUserIdentifier()
    {
      return _uid != null ? _uid : throw new InvalidRequestException("UserIdentifier in the request is invalid");
    }
  }
}