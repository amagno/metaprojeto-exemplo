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

    protected ActionLog()
    {
      Date = DateTime.Now;
    }
    public ActionLog(int actionLogTypeId) : this()
    {
      ActionLogTypeId = actionLogTypeId;
    }
    public ActionLog(int actionLogTypeId, Guid identifier) : this(actionLogTypeId)
    {
      UserIdentifier = identifier;
    }
    public ActionLog(int actionLogTypeId, string ipAddress) : this(actionLogTypeId)
    {
      IpAddress = ipAddress;
    }
    public ActionLog(int actionLogTypeId, Guid identifier, string ipAddress) : this(actionLogTypeId, identifier)
    {
      IpAddress = ipAddress;
    }
  }
}