using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.Common;

namespace MetaProjetoExemplo.Application.Services.Common
{
  public interface IActionLogService
  {
    Task RegisterLogAsync(ActionLogType type);
    Task RegisterLogAsync(ActionLogType type, string ipAdresss);
    Task RegisterLogAsync(ActionLogType type, Guid userIndetifier);
    Task RegisterLogAsync(ActionLogType type, Guid userIndetifier, string ipAdresss);
  }
}