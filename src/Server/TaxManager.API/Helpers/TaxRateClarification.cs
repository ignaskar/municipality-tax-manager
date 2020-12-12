using System;
using System.Collections.Generic;
using TaxManager.Core.Entities;
using TaxManager.Core.Enums;

namespace TaxManager.API.Helpers
{
    public static class TaxRateClarification
    {
        public static double GetTaxRateForDate(this Municipality municipality, IEnumerable<TaxSchedule> taxSchedules, DateTime dateToCheck)
        {
            var containsDailyRate = false;
            var containsWeeklyRate = false; 
            var containsMonthlyRate = false;
            var containsYearlyRate = false;
            
            foreach (var taxSchedule in taxSchedules)
            {
                if (dateToCheck.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Daily)
                {
                    containsDailyRate = true;
                }

                if (dateToCheck.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Weekly)
                {
                    containsWeeklyRate = true;
                }

                if (dateToCheck.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Monthly)
                {
                    containsMonthlyRate = true;
                }


                if (dateToCheck.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Yearly)
                {
                    containsYearlyRate = true;
                }
            }

            return containsDailyRate ? municipality.DailyTaxRate :
                containsWeeklyRate ? municipality.WeeklyTaxRate ?? 0 :
                containsMonthlyRate ? municipality.MonthlyTaxRate :
                containsYearlyRate ? municipality.YearlyTaxRate : 0;
        }
    }
}