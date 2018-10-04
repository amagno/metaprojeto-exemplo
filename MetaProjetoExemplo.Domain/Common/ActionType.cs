using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
  public class ActionType : Enumeration
  {
    public static ActionType UserCreated = new ActionType(1, "USER_CREATED");
    public static ActionType UserLoginAttempt = new ActionType(2, "USER_LOGIN_ATTEMPT");
    public static ActionType UserLoginFail = new ActionType(3, "USER_LOGIN_FAIL");
    public static ActionType UserLoginSuccess = new ActionType(4, "USER_LOGIN_SUCCESS");
    public static ActionType UserCreatedProject = new ActionType(5, "USER_CREATE_PROJECT");
    public static ActionType UserFinalizedProject = new ActionType(6, "USER_FINALIZED_PROJECT");
    public ActionType(int id, string name) : base(id, name)
    {
    }

    
  }
}