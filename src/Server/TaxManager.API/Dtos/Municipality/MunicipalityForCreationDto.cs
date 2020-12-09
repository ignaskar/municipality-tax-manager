using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaxManager.API.Dtos.TaxSchedule;

namespace TaxManager.API.Dtos.Municipality
{
    public class MunicipalityForCreationDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please provide a value greater than or equal to 0.1")]
        public double YearlyTaxRate { get; set; }
        
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please provide a value greater than or equal to 0.1")]
        public double MonthlyTaxRate { get; set; }
        
        [Range(0.1, double.MaxValue, ErrorMessage = "Please provide a value greater than or equal to 0.1")]
        public double? WeeklyTaxRate { get; set; }
        
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please provide a value greater than or equal to 0.1")]
        public double DailyTaxRate { get; set; }
        public ICollection<TaxScheduleDto> TaxSchedules { get; set; } = new List<TaxScheduleDto>();
}
}