using System;
using System.Linq;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace MetaProjetoExemplo.Infrastructure.Repositories.Common
{
  public class UserRepository : EntityRepository<User>, IUserRepository
  {
    public UserRepository(ExampleAppContext dbContext) : base(dbContext)
    {
    }
    public User Add(User user)
    {
      return _entity.Add(user).Entity;
    }
    public Task<User> GetByEmailAsync(string email)
    {
      return _entity.SingleOrDefaultAsync(u => u.Email == email);
    }
    public Task<User> GetByIdentifierAsync(Guid indetifier)
    {
      return _entity.SingleOrDefaultAsync(u => u.Identifier == indetifier);
    }
  }
}