using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Events;

namespace MetaProjetoExemplo.Application.Events
{
  public class ActionLogEventHandler : INotificationHandler<ActionEvent>
  {
    private readonly IActionRepository  _actionRepository;
    public ActionLogEventHandler(IActionRepository actionRepository)
    {
      _actionRepository = actionRepository;
    }

    public async Task Handle(ActionEvent notification, CancellationToken cancellationToken)
    {
      _actionRepository.Add(new Action(
        notification.ActionType.Id, 
        notification.Description,  
        notification.IpAddress,
        notification.UserIdentifier
      ));

      await _actionRepository.UnitOfWork.SaveChangesAsync();
    }
  }
}