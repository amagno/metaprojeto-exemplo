using MetaProjetoExemplo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaProjetoExemplo.Infrastructure.EntityConfiguration
{
  public class ActionLogTypeEntityConfig : IEntityTypeConfiguration<ActionType>
  {
    public void Configure(EntityTypeBuilder<ActionType> builder)
    {
      builder.ToTable("actions_logs_types", ExampleAppContext.COMMON_SCHEMA);
      builder.HasIndex(u => u.Id);
      builder.Property(a => a.Name).IsRequired();
    }
  }
}