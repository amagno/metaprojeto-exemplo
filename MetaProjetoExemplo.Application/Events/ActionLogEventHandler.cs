using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Events;

namespace MetaProjetoExemplo.Application.Events
{
  public class ActionLogEventHandler : INotificationHandler<ActionLogEvent>
  {
    private readonly IActionLogRepository  _actionLogRepository;
    public ActionLogEventHandler(IActionLogRepository actionLogRepository)
    {
      _actionLogRepository = actionLogRepository;
    }

    public async Task Handle(ActionLogEvent notification, CancellationToken cancellationToken)
    {
      _actionLogRepository.Add(new ActionLog(
        notification.ActionLogType.Id, 
        notification.Description,  
        notification.IpAddress,
        notification.UserIdentifier
      ));

      await _actionLogRepository.UnitOfWork.SaveChangesAsync();
    }
  }
}