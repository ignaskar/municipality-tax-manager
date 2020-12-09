using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxManager.Core.Entities;

namespace TaxManager.Infra.Data.Config
{
    public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.YearlyTaxRate).IsRequired();
            builder.Property(x => x.MonthlyTaxRate).IsRequired();
            builder.Property(x => x.DailyTaxRate).IsRequired();
        }
    }
}