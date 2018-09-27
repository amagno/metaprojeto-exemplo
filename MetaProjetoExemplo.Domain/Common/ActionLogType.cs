using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
  public class ActionLogType : Enumeration
  {
    public static ActionLogType UserCreated = new ActionLogType(1, "USER_CREATED");
    public static ActionLogType UserLoginAttempt = new ActionLogType(2, "USER_LOGIN_ATTEMPT");
    public static ActionLogType UserLoginFail = new ActionLogType(3, "USER_LOGIN_FAIL");
    public static ActionLogType UserLoginSuccess = new ActionLogType(4, "USER_LOGIN_SUCCESS");
    public ActionLogType(int id, string name) : base(id, name)
    {
    }

    
  }
}