using MetaProjetoExemplo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaProjetoExemplo.Infrastructure.EntityConfiguration
{
  public class ActionLogEntityConfig : IEntityTypeConfiguration<ActionLog>
  {
    public void Configure(EntityTypeBuilder<ActionLog> builder)
    {
      builder.ToTable("actions_logs", ExampleAppContext.COMMON_SCHEMA);
      builder.HasIndex(u => u.Id);

      // builder
      //   .HasOne<User>()
      //   .WithMany()
      //   .HasForeignKey(a => a.UserId)
      //   .IsRequired(false);
      
      builder
        .HasOne<ActionLogType>()
        .WithMany()
        .HasForeignKey(a => a.ActionLogTypeId)
        .IsRequired();


      builder.Property(u => u.IpAddress).IsRequired(false);
    }
  }
}