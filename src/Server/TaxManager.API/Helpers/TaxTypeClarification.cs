using System;
using TaxManager.Core.Entities;
using TaxManager.Core.Enums;

namespace TaxManager.API.Helpers
{
    public static class TaxTypeClarification
    {
        public static TaxType GetCorrectTaxType(this TaxSchedule schedule, int count = 0)
        {
            if (count == 0 && schedule.TaxType == TaxType.Yearly)
            {
                GetCorrectTaxType(schedule, ++count);
            }
            else if (count == 1 && schedule.TaxType == TaxType.Monthly)
            {
                GetCorrectTaxType(schedule, ++count);
            }
            else if (count == 2 && schedule.TaxType == TaxType.Weekly)
            {
                GetCorrectTaxType(schedule, ++count);
            }
            else if (count == 3 && schedule.TaxType == TaxType.Daily)
            {
                return schedule.TaxType;
            }

            return schedule.TaxType;
        }
    }
}