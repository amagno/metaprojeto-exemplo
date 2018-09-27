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
    protected string _ipRequest => HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress.ToString() : "no_ip_info";
    protected Guid _userIdentifier => HttpContext.GetUserIdentifier();
    protected async Task<ActionResult<T>> ExecuteServiceAsync<T>(Func<Task<T>> func)
    {
      try
      {
        T result = await func();
        return Ok(result);
      }
      catch (InvalidRequestException e)
      {
        return BadRequest(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
    protected async Task<T> SendCommand<T>(IRequest<T> command)
    {
      var mediator = HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
      return await mediator.Send(command);
    }
  }
}