using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Core.Models;

namespace Todo.Api.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            // Email obrigatório
            builder.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            // Nome obrigatório
            builder.Property(u => u.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            // Senha obrigatória
            builder.Property(u => u.PasswordHash)
                  .IsRequired();

            // Campo de criação com valor padrão
            builder.Property(u => u.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()"); // Define o valor default de CreatedAt

            builder.Property(u => u.LastLogin)
                  .IsRequired(false);
        }
    }
}
