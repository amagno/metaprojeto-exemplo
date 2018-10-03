using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Services.Common;
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
        /// <param name="command"></param>
        /// <param name="authService"></param>
        /// <returns>Retorna token JWT de autenticação</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthData>> Login([FromBody] UserLoginCommand command, [FromServices] IAuthService authService)
        {
            return await SendCommandAsync(new IpInfoCommand<UserLoginCommand, AuthData>(command, _ipRequest));
        }
    }
}
