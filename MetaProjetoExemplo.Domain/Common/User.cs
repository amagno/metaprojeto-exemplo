using System;
using System.Collections.Generic;
using MetaProjetoExemplo.Domain.Core;

namespace MetaProjetoExemplo.Domain.Common
{
  public class User : Entity
  {
    public Guid Identifier { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    // Construtor para o entity framework
    protected User() 
    {
      Identifier = Guid.NewGuid();
    }

    public User(string name, string email, string password) : this()
    {
      Name = name;
      Email = email;
      Password = password;
    }

  }
}