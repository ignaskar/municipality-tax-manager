using System.Collections.Generic;
using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class MunicipalitySpecParams
    {
        public string? MunicipalityName { get; set; }
        public double? WeeklyTaxRate { get; set; }
        public ICollection<TaxSchedule> TaxSchedules { get; set; }
    }
}