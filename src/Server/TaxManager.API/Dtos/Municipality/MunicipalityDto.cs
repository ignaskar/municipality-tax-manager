using System.Collections.Generic;
using TaxManager.API.Dtos.TaxSchedule;

namespace TaxManager.API.Dtos.Municipality
{
    public class MunicipalityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double YearlyTaxRate { get; set; }
        public double MonthlyTaxRate { get; set; }
        public double? WeeklyTaxRate { get; set; }
        public double DailyTaxRate { get; set; }
        public ICollection<TaxScheduleDto> TaxSchedules { get; set; } = new List<TaxScheduleDto>();
    }
}