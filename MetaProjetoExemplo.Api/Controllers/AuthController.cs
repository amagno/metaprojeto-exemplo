using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MetaProjetoExemplo.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        /// <summary>
        /// Realiza login com token JWT
        /// </summary>
        /// <param name="loginData"></param>
        /// <param name="authService"></param>
        /// <returns>Retorna token JWT de autenticação</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] Login loginData, [FromServices] IAuthService authService)
        {
          var ip = HttpContext.Connection.RemoteIpAddress != null ? HttpContext.Connection.RemoteIpAddress.ToString() : "no_ip_info";
          return await ExecuteServiceAsync<string>(() => authService.LoginAsync(loginData, ip));
        }
    }
}
