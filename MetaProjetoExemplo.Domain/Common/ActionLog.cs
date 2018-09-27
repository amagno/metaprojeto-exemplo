using System;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
 
  public class ActionLog : Entity
  {
    public DateTime Date { get; private set; }
    public Guid? UserIdentifier { get; private set; }
    public string IpAddress { get; private set; }
    public int ActionLogTypeId { get; private set; }
    public string Description { get; private set; }

    protected ActionLog()
    {
      Date = DateTime.Now;
    }
    public ActionLog(int actionLogTypeId, string description, string ipAddress, Guid? identifier) : this()
    {
      ActionLogTypeId = actionLogTypeId;
      Description = description ?? string.Empty;
      UserIdentifier = identifier;
      IpAddress = ipAddress ?? string.Empty;
    }
  }
}