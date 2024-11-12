using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Core.Models;

namespace Todo.Infrastruture.Data.Mappings
{
    public class LogEntryMap : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Level).IsRequired();
            builder.Property(e => e.Timestamp).IsRequired();
            builder.Property(e => e.Message).IsRequired();
            builder.Property(e => e.Source);
            builder.Property(e => e.Details);
        }
    }
}
