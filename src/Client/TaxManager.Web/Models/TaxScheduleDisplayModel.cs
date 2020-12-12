using System;
using System.ComponentModel.DataAnnotations;

namespace TaxManager.Web.Models
{
    public class TaxScheduleDisplayModel
    {
        public int Id { get; set; }
        
        [Required]
        public int MunicipalityId { get; set; }
        
        [Required]
        [Range(typeof(DateTime), "1/1/2000", "12/31/2999", ErrorMessage = "Please enter a date between 2000-01-01 and 2999-12-31")]
        public DateTime StartDate { get; set; }
        
        [Required]
        [Range(typeof(DateTime), "1/1/2000", "12/31/2999", ErrorMessage = "Please enter a date between 2000-01-01 and 2999-12-31")]
        public DateTime EndDate { get; set; }
        
        [Required]
        public string TaxType { get; set; }
    }
}