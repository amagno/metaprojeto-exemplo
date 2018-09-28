using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Attributes;
using MetaProjetoExemplo.Application.Commands;
using MetaProjetoExemplo.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MetaProjetoExemplo.Api.Controllers
{
    [Route("api/project-management")]
    [ApiController]
    public class ProjectManagementController : ApiControllerBase
    {
      /// <summary>
      /// Criar novo projeto para usuário logado
      /// </summary>
      /// <param name="command"></param>
      /// <returns></returns>
      [HttpPost]
      [JwtAuthorize]
      public async Task<ActionResult<int>> CreateProject(
        [FromBody] CreateProjectCommand command
        )
      {
        return await SendCommandAsync(new AuthenticatedCommand<CreateProjectCommand, int>(command, _userIdentifier));
      }
      /// <summary>
      /// Projetos do usuário logado
      /// </summary>
      /// <param name="queries"></param>
      /// <returns></returns>
      [HttpGet]
      [JwtAuthorize]
      public async Task<ActionResult<ProjectManagerViewModel>> GetUserProjects([FromServices] IProjectManagerQueries queries)
      {
        return await queries.GetUserProjectManager(_userIdentifier);
      }
    }
}
