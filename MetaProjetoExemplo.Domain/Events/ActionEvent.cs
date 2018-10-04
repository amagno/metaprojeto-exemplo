using System;
using MediatR;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Events
{
  public class ActionEvent : INotification
  {
    public ActionType ActionType { get; private set; }
    public Guid? UserIdentifier { get; private set; }
    public string Description { get; private set; }
    public string IpAddress { get; private set; }
    public ActionEvent(ActionType type, string description = null, string ipAddress = null)
    {
      ActionType = type;
      Description = description;
      IpAddress = ipAddress;
    }
    public ActionEvent(ActionType type, string description, string ipAddress, Guid? identifier) : this(type, description, ipAddress)
    {
      UserIdentifier = identifier;
    }


  }
  public class LoginSuccessActionEvent : ActionEvent
  {
    public LoginSuccessActionEvent(Guid identifier, string ipAddress) : 
      base(ActionType.UserLoginSuccess, null, ipAddress, identifier)
    {
    }
  }
  public class LoginAttemptActionEvent : ActionEvent
  {
    public LoginAttemptActionEvent(string ipAddress) : 
      base(ActionType.UserLoginAttempt, null, ipAddress)
    {
    }
  }
  public class LoginFailActionEvent : ActionEvent
  {
    public LoginFailActionEvent(string ipAddress) : 
      base(ActionType.UserLoginFail, null, ipAddress)
    {
    }
  }
  public class ProjectCreatedActionEvent : ActionEvent
  {
    public ProjectCreatedActionEvent(Guid identifier) : 
      base(ActionType.UserCreatedProject, null, null, identifier)
    {
    }
  }
  public class ProjectFinalizedActionEvent : ActionEvent
  {
    public ProjectFinalizedActionEvent(Guid identifier) : 
      base(ActionType.UserFinalizedProject, null, null, identifier)
    {
    }
  }
}