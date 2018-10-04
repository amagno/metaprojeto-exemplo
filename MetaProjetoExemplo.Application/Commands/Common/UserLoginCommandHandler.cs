using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.Events;

namespace MetaProjetoExemplo.Application.Commands.Common
{
  public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, AuthData>
  {
    private readonly IMediator _mediator;
    private readonly IAuthService _auth;
    public UserLoginCommandHandler(IMediator mediator, IAuthService auth)
    {
      _mediator = mediator;
      _auth = auth;
    }
    public async Task<AuthData> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
      try
      {
        await _mediator.Publish(new LoginAttemptActionEvent(request.IpInfo));
        var result = await _auth.LoginAsync(request.Email, request.Password);
        await _mediator.Publish(new LoginSuccessActionEvent(result.UserIdentifier, request.IpInfo));
        return result;
      }
      catch(InvalidRequestException e)
      {
        await _mediator.Publish(new LoginFailActionEvent(request.IpInfo));
        throw e;
      }
    }
  }
}