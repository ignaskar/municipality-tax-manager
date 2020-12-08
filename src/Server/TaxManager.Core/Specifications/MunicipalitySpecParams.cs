using System.Collections.Generic;
using TaxManager.Core.Entities;

namespace TaxManager.Core.Specifications
{
    public class MunicipalitySpecParams
    {
        public double? WeeklyTaxRate { get; set; }
        public ICollection<TaxSchedule> Schedules { get; set; }
    }
}