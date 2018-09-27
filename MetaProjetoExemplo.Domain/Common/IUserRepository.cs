using System;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
  public interface IUserRepository : IRepository
  {
      User Add(User user);
      Task<User> GetByEmailAsync(string email);
      Task<User> GetByIdentifierAsync(Guid indetifier);
  }
}