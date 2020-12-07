using System.Collections.Generic;

namespace TaxManager.Core.Entities
{
    public class Municipality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double YearlyTaxRate { get; set; }
        public double MonthlyTaxRate { get; set; }
        public double? WeeklyTaxRate { get; set; }
        public double DailyTaxRate { get; set; }
        public ICollection<TaxSchedule> TaxSchedules { get; set; } = new List<TaxSchedule>();
    }
}