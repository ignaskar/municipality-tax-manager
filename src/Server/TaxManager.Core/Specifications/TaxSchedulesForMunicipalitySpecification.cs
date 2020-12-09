using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class TaxSchedulesForMunicipalitySpecification : BaseSpecification<TaxSchedule>
    {
        public TaxSchedulesForMunicipalitySpecification(int id) : base (x => x.Id == id)
        {
            
        }
    }
}