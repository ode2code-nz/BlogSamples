using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Model.ToDos;

namespace Todo.Infrastructure.Data.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.ToTable(nameof(ToDoItem));

            builder.Property(x => x.Id)
                .HasColumnName($"{nameof(ToDoItem)}Id");

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(400);

            // Use EF value conversions for single-property value objects
            builder.Property(t => t.Email)
                .HasConversion(p => p.Value, p => Email.Create(p).Value)
                .HasMaxLength(250);
        }
    }
}