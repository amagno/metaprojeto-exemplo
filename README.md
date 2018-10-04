# MetaProjeto de Testes

## Exemplo de aplicação utilizando DDD e CQRS

Requer .NET Core SDK 2.1.3

> altere a connectionString da classe **MetaProjetoExemplo.Infrastructure.ExampleAppContextDesignFactory**

```csharp
public class ExampleAppContextDesignFactory : IDesignTimeDbContextFactory<ExampleAppContext>
  {
    public ExampleAppContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ExampleAppContext>()
        .UseSqlServer("Data Source=localhost; Initial Catalog=your_database; Integrated Security=false; User Id=your_user; Password=your_password;");

      return new ExampleAppContext(optionsBuilder.Options);
    }
  }
```

> realize a migração do banco de dados

```terminal
cd MetaProjetoExemplo.Infrastructure
dotnet ef database update
```

> para excutar a aplicação, adicione sua conexão em **MetaProjetoExemplo.Api/appsettings.Development.json**

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Integrated Security=false;Database=your_database;User=your_user;Password=your_password"
}
```

> excutar

```terminal
cd MetaProjetoExemplo.Api
dotnet run
```