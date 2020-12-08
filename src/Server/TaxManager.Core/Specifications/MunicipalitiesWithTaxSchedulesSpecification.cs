using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class MunicipalitiesWithTaxSchedulesSpecification : BaseSpecification<Municipality>
    {
        public MunicipalitiesWithTaxSchedulesSpecification(MunicipalitySpecParams municipalityParams)
            : base(x => 
                !municipalityParams.WeeklyTaxRate.HasValue || x.WeeklyTaxRate == municipalityParams.WeeklyTaxRate &&
                string.IsNullOrEmpty(municipalityParams.MunicipalityName) || x.Name == municipalityParams.MunicipalityName)
        {
            AddInclude(x => x.TaxSchedules);
        }

        public MunicipalitiesWithTaxSchedulesSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.TaxSchedules);
        }

        public MunicipalitiesWithTaxSchedulesSpecification(string name) : base(x => x.Name == name)
        {
            AddInclude(x => x.TaxSchedules);
        }
    }
}