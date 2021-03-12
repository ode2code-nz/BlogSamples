using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiSample.Domain.Model.ToDos;

namespace ApiSample.Infrastructure.Data.Configurations
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
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