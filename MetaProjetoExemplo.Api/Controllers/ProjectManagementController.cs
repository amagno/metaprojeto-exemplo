using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Attributes;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Extesions;
using MetaProjetoExemplo.Application.Services;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Application.ViewModels;
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
      /// <param name="newProjectData"></param>
      /// <param name="projectManagementService"></param>
      /// <returns></returns>
      [HttpPost]
      [JwtAuthorize]
      public async Task<ActionResult<ProjectCreated>> CreateProject(
        [FromBody] NewProject newProjectData, 
        [FromServices] IProjectManagementService projectManagementService
        )
      {
        return await ExecuteServiceAsync(() => projectManagementService.CreateProject(_userIdentifier, newProjectData));
      }
      /// <summary>
      /// Projetos do usuário logado
      /// </summary>
      /// <param name="projectManagementService"></param>
      /// <returns></returns>
      [HttpGet]
      [JwtAuthorize]
      public async Task<ActionResult<ProjectManagerItem>> GetUserProjects([FromServices] IProjectManagementService projectManagementService)
      {
        return await ExecuteServiceAsync(() => projectManagementService.GetUserProjects(_userIdentifier));
      }
    }
}
