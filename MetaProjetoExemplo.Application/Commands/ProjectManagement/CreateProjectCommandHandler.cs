

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.Events;
using MetaProjetoExemplo.Domain.ProjectManagement;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, bool>
  {
    private readonly IProjectManagerRepository _projectManagerRepository;
    private readonly IMediator _mediator;
    public CreateProjectCommandHandler(IMediator mediator, IProjectManagerRepository projectManagerRepository)
    {
      _mediator = mediator;
      _projectManagerRepository = projectManagerRepository;
    }
    public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var pm = await _projectManagerRepository.GetByUserIdentifierAsync(request.UserIdentifier);
        var exists = (pm != null);

        if (!exists)
        {
          pm = new ProjectManager(request.UserIdentifier);
        }
        
        pm.AddProject(request.Title, request.StartDate, request.FinishDate);
        var entity = exists ?
           _projectManagerRepository.Update(pm) :
           _projectManagerRepository.Add(pm);
        // realizar commit antes de publicar os eventos
        var result = await _projectManagerRepository.UnitOfWork.CommitAsync();
        await _mediator.Publish(new ProjectCreatedActionEvent(request.UserIdentifier));

        return result;
      }
      catch (InvalidProjectDateDomainException e)
      {
        throw new InvalidRequestException(ErrorCode.InvalidProjectDate, e.Message);
      }
      catch (DomainException e)
      {
        throw new InvalidRequestException(ErrorCode.NoErrorCode, e.Message);
      }
    }
  }
}