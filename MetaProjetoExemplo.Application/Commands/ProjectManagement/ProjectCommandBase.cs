using System;

namespace MetaProjetoExemplo.Application.Commands.ProjectManagement
{
  public abstract class ProjectCommandBase
  {
    private Guid _userIdentifier;
    public Guid UserIdentifier { get => _userIdentifier != null ? _userIdentifier : throw new ArgumentNullException(nameof(UserIdentifier)); }
    private string _ipInfo;
    public string IpInfo { get => _ipInfo ?? "no_ip_info"; }
    public ProjectCommandBase(Guid userIdentifier, string ipInfo)
    {
      _userIdentifier = userIdentifier;
      _ipInfo = ipInfo;
    }
    public void SetRequestInfo(Guid userIdentifier, string ipInfo)
    {
      _userIdentifier = userIdentifier;
      _ipInfo = ipInfo;
    }
    public void SetRequestInfo(Guid userIdentifier)
    {
      _userIdentifier = userIdentifier;
    }
  }
}