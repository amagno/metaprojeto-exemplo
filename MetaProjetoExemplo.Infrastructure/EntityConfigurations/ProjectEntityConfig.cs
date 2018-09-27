using System;
using MetaProjetoExemplo.Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaProjetoExemplo.Infrastructure.EntityConfiguration
{
  public class ProjectEntityConfig : IEntityTypeConfiguration<Project>
  {
    public void Configure(EntityTypeBuilder<Project> builder)
    {
      builder.ToTable("projects", ExampleAppContext.PROJECT_MANAGEMENT_SCHEMA);

      builder.HasKey(p => p.Id);

      builder.Property(p => p.Title).IsRequired();

      builder.Property(p => p.StartDate).IsRequired();

      builder.Property(p => p.FinishDate).IsRequired();

      builder.Property(p => p.IsActive).IsRequired();

      builder
        .Property(p => p.CreatedDate)
        .IsRequired();

      builder
        .HasOne(p => p.Manager)
        .WithMany(pm => pm.Projects)
        .HasForeignKey("project_manager_id")
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}