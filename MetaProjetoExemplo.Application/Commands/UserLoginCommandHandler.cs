using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.Events;

namespace MetaProjetoExemplo.Application.Commands
{
  public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, string>
  {
    private readonly IMediator _mediator;
    private readonly IAuthService _auth;
    public UserLoginCommandHandler(IMediator mediator, IAuthService auth)
    {
      _mediator = mediator;
      _auth = auth;
    }
    public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
      try
      {
        await _mediator.Publish(new ActionLogEvent(ActionLogType.UserLoginAttempt, request.GetIp()));
        return await _auth.LoginAsync(request.Email, request.Password);
      }
      catch(InvalidRequestException e)
      {
        await _mediator.Publish(new ActionLogEvent(ActionLogType.UserLoginFail, request.GetIp(), e.Message));
        throw e;
      }
    }
  }
}