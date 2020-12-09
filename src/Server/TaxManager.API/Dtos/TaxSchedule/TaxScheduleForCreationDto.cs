using System;
using System.ComponentModel.DataAnnotations;

namespace TaxManager.API.Dtos.TaxSchedule
{
    public class TaxScheduleForCreationDto
    {
        [Required]
        public int MunicipalityId { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public string TaxType { get; set; }
    }
}