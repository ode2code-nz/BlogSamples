using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApiSample.Domain.Model.MasterTradingAgreements;

namespace ApiSample.Infrastructure.Data.Configurations
{
    public class MasterTradingAgreementConfiguration : IEntityTypeConfiguration<MasterTradingAgreement>
    {
        public void Configure(EntityTypeBuilder<MasterTradingAgreement> builder)
        {
            builder.ToTable(nameof(MasterTradingAgreement));

            builder.Property(x => x.Id)
                .HasColumnName($"{nameof(MasterTradingAgreement)}Id");

            //builder.HasMany(c => c.ContractSchedules)
            //    .WithOne(x => x.MasterTradingAgreement)
            //    .HasForeignKey(x => x.MasterTradingAgreementId);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Comments)
                .HasMaxLength(400);

            builder.OwnsOne(
                o => o.Duration,
                sa =>
                {
                    sa.Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime");
                    sa.Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime");
                });

            builder
                .HasOne(x => x.Counterparty)
                .WithMany()
                .HasForeignKey("CounterpartyId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}