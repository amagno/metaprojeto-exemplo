using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MetaProjetoExemplo.Api.Controllers
{
  public abstract class ApiControllerBase : ControllerBase
  {
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
  }
}