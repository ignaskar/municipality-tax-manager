using System;
using System.Collections.Generic;
using TaxManager.Core.Entities;
using TaxManager.Core.Enums;

namespace TaxManager.API.Helpers
{
    public static class TaxRateClarification
    {
        public static double GetTaxRateForDate(this Municipality municipality, ICollection<TaxSchedule> taxSchedules, DateTime dateToCheck)
        {
            var containsDailyRate = false;
            var containsWeeklyRate = false; 
            var containsMonthlyRate = false;
            
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
            }

            return containsDailyRate ? municipality.DailyTaxRate :
                containsWeeklyRate ? municipality.WeeklyTaxRate ?? 0 :
                !containsMonthlyRate ? municipality.YearlyTaxRate :
                municipality.MonthlyTaxRate;
        }
    }
}