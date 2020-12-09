using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxManager.Core.Entities;

namespace TaxManager.Infra.Data.Config
{
    public class TaxScheduleConfiguration : IEntityTypeConfiguration<TaxSchedule>
    {
        public void Configure(EntityTypeBuilder<TaxSchedule> builder)
        {
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.MunicipalityId).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.TaxType).IsRequired();
        }
    }
}