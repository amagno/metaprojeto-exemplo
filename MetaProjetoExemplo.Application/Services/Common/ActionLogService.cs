using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Application.Services.Common
{
  public class ActionLogService : IActionLogService
  {
    private readonly IActionLogRepository _actionLogRepository;
    public ActionLogService(IActionLogRepository actionLogRepository)
    {
      _actionLogRepository = actionLogRepository;
    }
    /// <summary>
    /// Registrar log de atividade do usuário
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task RegisterLogAsync(ActionLogType type)
    {
      var log = new ActionLog(type.Id);
      _actionLogRepository.Add(log);

      await _actionLogRepository.UnitOfWork.SaveChangesAsync();
    }
    /// <summary>
    /// Registrar log de atividade do usuário
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ipAdresss"></param>
    /// <returns></returns>
    public async Task RegisterLogAsync(ActionLogType type, string ipAdresss)
    {
      var log = new ActionLog(type.Id, ipAdresss);
      _actionLogRepository.Add(log);

      await _actionLogRepository.UnitOfWork.SaveChangesAsync();
    }
    /// <summary>
    /// Registrar log de atividade do usuário
    /// </summary>
    /// <param name="type"></param>
    /// <param name="userIndetifier"></param>
    /// <returns></returns>
    public async Task RegisterLogAsync(ActionLogType type, Guid userIndetifier)
    {
      var log = new ActionLog(type.Id, userIndetifier);
      _actionLogRepository.Add(log);

      await _actionLogRepository.UnitOfWork.SaveChangesAsync();
    }
    /// <summary>
    /// Registrar log de atividade do usuário
    /// </summary>
    /// <param name="type"></param>
    /// <param name="userIndetifier"></param>
    /// <param name="ipAdresss"></param>
    /// <returns></returns>
    public async Task RegisterLogAsync(ActionLogType type, Guid userIndetifier, string ipAdresss)
    {
      var log = new ActionLog(type.Id, userIndetifier, ipAdresss);
      _actionLogRepository.Add(log);

      await _actionLogRepository.UnitOfWork.SaveChangesAsync();
    }
  }
}