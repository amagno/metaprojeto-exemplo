using System;
using MediatR;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Events
{
  public class ActionLogEvent : INotification
  {
    public ActionLogType ActionLogType { get; private set; }
    public Guid? UserIdentifier { get; private set; }
    public string Description { get; private set; }
    public string IpAddress { get; private set; }
    public ActionLogEvent(ActionLogType type, string description = null, string ipAddress = null)
    {
      ActionLogType = type;
      Description = description;
      IpAddress = ipAddress;
    }
    public ActionLogEvent(ActionLogType type, string description, string ipAddress, Guid? identifier) : this(type, description, ipAddress)
    {
      UserIdentifier = identifier;
    }
  }
}