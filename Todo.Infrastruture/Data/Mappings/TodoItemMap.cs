using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Core.Models;

namespace Todo.Api.Data.Mappings
{
    public class TodoItemMap : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder) 
        {
            builder.ToTable("TodoItems");

            builder.HasKey(t => t.Id);

            
            builder.HasOne(t => t.User)
                  .WithMany(u => u.TodoItems)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            
            builder.Property(t => t.Title)
                          .IsRequired()
                          .HasMaxLength(200); 

            builder.Property(t => t.Description)
                          .HasMaxLength(500); 
            builder.Property(t => t.CreatedAt)
                          .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.IsCompleted)
                          .IsRequired()
                          .HasDefaultValue(false);

            builder.Property(t => t.DueDate)
                          .IsRequired(false);


        }
    }
}
