using System;
using MediatR;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public class FinalizeProjectCommand : ProjectCommandBase, IRequest<bool>
  {
    public int Id { get; set; }
    public FinalizeProjectCommand(int id, Guid userIdentifier, string ipInfo) : base(userIdentifier, ipInfo)
    {
      Id = id;
    }

  }
}