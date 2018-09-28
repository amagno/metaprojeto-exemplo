

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.Events;
using MetaProjetoExemplo.Domain.ProjectManagement;

namespace MetaProjetoExemplo.Application.Commands
{
  public class CreateProjectCommandHandler : IRequestHandler<AuthenticatedCommand<CreateProjectCommand, int>, int>
  {
    private readonly IProjectManagerRepository _projectManagerRepository;
    private readonly IMediator _mediator;
    public CreateProjectCommandHandler(IMediator mediator, IProjectManagerRepository projectManagerRepository)
    {
      _mediator = mediator;
      _projectManagerRepository = projectManagerRepository;
    }
    public async Task<int> Handle(AuthenticatedCommand<CreateProjectCommand, int> request, CancellationToken cancellationToken)
    {
      try
      {
        var pm = await _projectManagerRepository.GetByUserIdentifierAsync(request.UserIdentifier);
        var exists = (pm != null);

        if (!exists)
        {
          pm = new ProjectManager(request.UserIdentifier);
        }
        pm.AddProject(request.Command.Title, request.Command.StartDate, request.Command.FinishDate);
        var result = exists ?
           _projectManagerRepository.Update(pm) :
           _projectManagerRepository.Add(pm);

        await _projectManagerRepository.UnitOfWork.SaveChangesAsync();
        await _mediator.Publish(new ProjectCreatedActionEvent(pm.UserIdentifier));

        return result.Id;
      }
      catch (DomainException e)
      {
        throw new InvalidRequestException(e.Message);
      }
    }
  }
}