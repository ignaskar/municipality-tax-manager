using System;

namespace TaxManager.API.Dtos.TaxSchedule
{
    public class TaxScheduleDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TaxType { get; set; }
    }
}