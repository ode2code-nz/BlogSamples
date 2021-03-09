using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Model.StaticData;

namespace ToDo.Infrastructure.Data.Configurations.StaticData
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company));

            builder.Property(x => x.Id)
                .HasColumnName($"{nameof(Company)}Id");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}