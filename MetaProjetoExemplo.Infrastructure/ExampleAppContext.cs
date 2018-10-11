using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Domain.Core;
using MetaProjetoExemplo.Domain.ProjectManagement;
using MetaProjetoExemplo.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MetaProjetoExemplo.Infrastructure
{
  public class ExampleAppContext : DbContext, IUnitOfWork, IContextConnection
  {
    public const string PROJECT_MANAGEMENT_SCHEMA = "project_management";
    public const string COMMON_SCHEMA = "common";
    // 
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Action> Actions { get; set; }

    public ExampleAppContext(DbContextOptions<ExampleAppContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ProjectEntityConfig());
      modelBuilder.ApplyConfiguration(new ProjectManagerEntityConfig());
      modelBuilder.ApplyConfiguration(new UserEntityConfig());
      modelBuilder.ApplyConfiguration(new ActionLogEntityConfig());
      modelBuilder.ApplyConfiguration(new ActionLogTypeEntityConfig());

      EntityColumnsToLowerCase(modelBuilder);
      SeedInitialData(modelBuilder);
    }
    /// <summary>
    /// Transforma colunas em camelcase para lowercase e underscore
    /// </summary>
    /// <param name="modelBuilder"></param>
    private void EntityColumnsToLowerCase(ModelBuilder modelBuilder)
    {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        foreach (var property in entityType.GetProperties())
        {
          var column = property.SqlServer().ColumnName;
          var select = column.Select((x,i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString());
          property.SqlServer().ColumnName = string.Concat(select).ToLower();
        }
      }
    }
    public IDbConnection GetConnection()
    {
      return base.Database.GetDbConnection();
    }
    private void SeedInitialData(ModelBuilder modelBuilder)
    {
      // Seed enumartion data;
      var entities = ActionType.GetAll<ActionType>().ToArray();
      modelBuilder.Entity<ActionType>().HasData(entities);
    }
    public async Task<bool> CommitAsync()
    {
      var result = await SaveChangesAsync();
      return result > 0;
    }
  }
  /// <summary>
  /// Clase para migração do contexto de banco de dados
  /// </summary>
  /// <typeparam name="ExampleAppContext"></typeparam>
  public class ExampleAppContextDesignFactory : IDesignTimeDbContextFactory<ExampleAppContext>
  {
    public ExampleAppContext CreateDbContext(string[] args)
    {
      var dbServer = string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("SQL_DATA_SOURCE")) ? "localhost" : System.Environment.GetEnvironmentVariable("SQL_DATA_SOURCE");
      var optionsBuilder = new DbContextOptionsBuilder<ExampleAppContext>()
        .UseSqlServer($"Data Source={dbServer}; Initial Catalog=example_app; Integrated Security=false; User Id=sa; Password=abc123##;");

      return new ExampleAppContext(optionsBuilder.Options);
    }
  }
}