using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class MunicipalityWithoutTaxSchedulesSpecification : BaseSpecification<Municipality>
    {
        public MunicipalityWithoutTaxSchedulesSpecification(string name) : base (x => x.Name == name)
        {
            
        }
    }
}