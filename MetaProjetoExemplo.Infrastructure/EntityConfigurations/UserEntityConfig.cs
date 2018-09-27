using MetaProjetoExemplo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaProjetoExemplo.Infrastructure.EntityConfiguration
{
  public class UserEntityConfig : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("users", ExampleAppContext.COMMON_SCHEMA);
      builder.HasIndex(u => u.Id);
      
      builder.Property(u => u.Name).IsRequired();
      builder.Property(u => u.Email).IsRequired();
      builder.Property(u => u.Password).IsRequired();
      builder.Property(u => u.Identifier).IsRequired();

      builder.HasIndex(u => u.Identifier).IsUnique();
      builder.HasIndex(u => u.Email).IsUnique();

    }
  }
}