using System;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Extesions;
using Microsoft.AspNetCore.Mvc;

namespace MetaProjetoExemplo.Api.Controllers
{
  public abstract class ApiControllerBase : ControllerBase
  {
    protected string _ipRequest => HttpContext.Connection.RemoteIpAddress != null ? 
      HttpContext.Connection.RemoteIpAddress.ToString() : 
      string.Empty;
    protected Guid _userIdentifier => HttpContext.GetUserIdentifier();
    protected async Task<ActionResult<T>> SendCommandAsync<T>(IRequest<T> command)
    {
      var mediator = HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
      try
      {
        T result = await mediator.Send(command);
        return Ok(result);
      }
      catch (InvalidRequestException e)
      {
        return BadRequest(e.Message);
      }
      catch (UnauthorizedException)
      {
        return Unauthorized();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}