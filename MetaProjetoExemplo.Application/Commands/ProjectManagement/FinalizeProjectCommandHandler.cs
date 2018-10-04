using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.Events;
using MetaProjetoExemplo.Domain.ProjectManagement;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public class FinalizeProjectCommandHandler : 
    IRequestHandler<AuthenticatedCommand<FinalizeProjectCommand, bool>, bool>
  {
    private readonly IMediator _mediator;
    private readonly IProjectManagerRepository _projectManagerRepository;
    public FinalizeProjectCommandHandler(
      IMediator mediator, 
      IProjectManagerRepository projectManagerRepository
      )
    {
      _projectManagerRepository = projectManagerRepository;
      _mediator = mediator;
    }

    public async Task<bool> Handle(AuthenticatedCommand<FinalizeProjectCommand, bool> request, CancellationToken cancellationToken)
    {
      var projectManager = await _projectManagerRepository.GetByUserIdentifierAsync(request.UserIdentifier);
      
      if (projectManager == null)
      {
        throw new InvalidProjectManagerException(request.UserIdentifier);
      }
      var project = projectManager
        .Projects
        .FirstOrDefault(p => p.IsActive && p.Id == request.Command.Id);

      if (project == null)
      {
        throw new InvalidProjectIdException(request.Command.Id);
      }
      try
      {
        project.FinalizeNow();
        _projectManagerRepository.Update(projectManager);
        // realizar commit antes de publicar os eventos
        var result = await _projectManagerRepository.UnitOfWork.CommitAsync();
        await _mediator.Publish(new ProjectFinalizedActionEvent(request.UserIdentifier));
        return result;
      }
      catch (DomainException e)
      {
        throw new InvalidRequestException(e.Message);
      }
    }
  }
}