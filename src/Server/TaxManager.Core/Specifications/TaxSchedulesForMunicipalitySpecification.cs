using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class TaxSchedulesForMunicipalitySpecification : BaseSpecification<TaxSchedule>
    {
        public TaxSchedulesForMunicipalitySpecification(TaxScheduleSpecParams scheduleParameters)
            : base (x => 
                scheduleParameters.MunicipalityId == 0 || x.MunicipalityId == scheduleParameters.MunicipalityId)
        {
            
        }

        public TaxSchedulesForMunicipalitySpecification(int id) : base (x => x.MunicipalityId == id)
        {
            
        }
    }
}