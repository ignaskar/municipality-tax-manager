using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class MunicipalitiesWithTaxSchedulesSpecification : BaseSpecification<Municipality>
    {
        public MunicipalitiesWithTaxSchedulesSpecification(MunicipalitySpecParams municipalityParams)
            : base(x => 
                !municipalityParams.WeeklyTaxRate.HasValue || x.WeeklyTaxRate == municipalityParams.WeeklyTaxRate)
        {
            AddInclude(x => x.TaxSchedules);
        }

        public MunicipalitiesWithTaxSchedulesSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.TaxSchedules);
        }
    }
}