using MetaProjetoExemplo.Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaProjetoExemplo.Infrastructure.EntityConfiguration
{
  public class ProjectManagerEntityConfig : IEntityTypeConfiguration<ProjectManager>
  {
    public void Configure(EntityTypeBuilder<ProjectManager> builder)
    {
      builder.ToTable("project_managers", ExampleAppContext.PROJECT_MANAGEMENT_SCHEMA);
      builder.HasIndex(pm => pm.Id);
      builder.Property(pm => pm.UserIdentifier).IsRequired();
    }
  }
}