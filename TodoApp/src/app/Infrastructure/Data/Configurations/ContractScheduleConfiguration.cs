using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Model.MasterTradingAgreements;

namespace ToDo.Infrastructure.Data.Configurations
{
    public class ContractScheduleConfiguration : IEntityTypeConfiguration<ContractSchedule>
    {
        public void Configure(EntityTypeBuilder<ContractSchedule> builder)
        {
            builder.ToTable(nameof(ContractSchedule));

            builder.Property(x => x.Id)
                .HasColumnName($"{nameof(ContractSchedule)}Id");

            builder.HasOne(d => d.MasterTradingAgreement)
                .WithMany(p => p.ContractSchedules)
                .HasForeignKey("MasterTradingAgreementId")
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.OwnsOne(
                o => o.Duration,
                sa =>
                {
                    sa.Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime");
                    sa.Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime");
                });

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Comments)
                .HasMaxLength(400);

            builder
                .HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey("LocationId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}