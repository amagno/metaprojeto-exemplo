using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.ProjectManagement;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public class FinalizeProjectCommandHandler : 
    IRequestHandler<AuthenticatedCommand<FinalizeProjectCommand, bool>, bool>
  {
    private readonly IMediator _mediator;
    private readonly IProjectManagerRepository _projectManagerRepository;
    public FinalizeProjectCommandHandler(IMediator mediator, IProjectManagerRepository projectManagerRepository)
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
        .FirstOrDefault(p => p.Id == request.Command.Id && p.IsActive);

      if (project == null)
      {
        throw new InvalidProjectIdException(request.Command.Id);
      }


      try
      {
        project.FinalizeThis();
        _projectManagerRepository.Update(projectManager);
        return await _projectManagerRepository.UnitOfWork.CommitAsync();
      }
      catch (DomainException e)
      {
        throw new InvalidRequestException(e.Message);
      }
    }
  }
}