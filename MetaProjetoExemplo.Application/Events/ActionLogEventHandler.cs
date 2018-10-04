using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Events;

namespace MetaProjetoExemplo.Application.Events
{
  public class ActionLogEventHandler : 
    INotificationHandler<ActionEvent>,
    INotificationHandler<LoginAttemptActionEvent>,
    INotificationHandler<LoginSuccessActionEvent>,
    INotificationHandler<LoginFailActionEvent>,
    INotificationHandler<ProjectCreatedActionEvent>,
    INotificationHandler<ProjectFinalizedActionEvent>

  {
    private readonly IActionRepository  _actionRepository;
    public ActionLogEventHandler(IActionRepository actionRepository)
    {
      _actionRepository = actionRepository;
    }

    public Task Handle(ActionEvent notification, CancellationToken cancellationToken)
    {
      return Persist(notification);
    }
    public Task Handle(ProjectFinalizedActionEvent notification, CancellationToken cancellationToken)
    {
      return Persist(notification);
    }
    public Task Handle(ProjectCreatedActionEvent notification, CancellationToken cancellationToken)
    {
      return Persist(notification);
    }
    public Task Handle(LoginFailActionEvent notification, CancellationToken cancellationToken)
    {
      return Persist(notification);
    }
    public Task Handle(LoginSuccessActionEvent notification, CancellationToken cancellationToken)
    {
      return Persist(notification);
    }
    public Task Handle(LoginAttemptActionEvent notification, CancellationToken cancellationToken)
    {
      return Persist(notification);
    }

    private async Task Persist(ActionEvent notification)
    {
      _actionRepository.Add(new Action(
        notification.ActionType.Id, 
        notification.Description,  
        notification.IpAddress,
        notification.UserIdentifier
      ));

      var result = await _actionRepository.UnitOfWork.CommitAsync();

      if (!result)
      {
        throw new System.Exception("error on persist action event");
      }
    }
  }
}