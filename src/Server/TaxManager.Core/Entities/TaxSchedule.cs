using System;
using TaxManager.Core.Enums;

namespace TaxManager.Core.Entities
{
    public class TaxSchedule
    {
        public int Id { get; set; }
        public Municipality Municipality { get; set; }
        public int MunicipalityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaxType TaxType { get; set; }
    }
}