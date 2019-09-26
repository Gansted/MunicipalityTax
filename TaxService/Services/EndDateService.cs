using System;
using TaxService.Models;

namespace TaxService.Services
{
  public class EndDateService : IEndDateService
  {
    //Users do not enter end dates, these are calculated by start date and time period to ensure consistensy between period and dates.
    public DateTime GetEndDate(DateTime startDate, TimePeriod period)
    {
      switch (period)
      {
        case TimePeriod.Day: return startDate;
        case TimePeriod.Week: return startDate.AddDays(6);
        case TimePeriod.Month: return startDate.AddMonths(1).AddDays(-1);
        case TimePeriod.Year: return startDate.AddYears(1).AddDays(-1);
        default: return startDate;
      }
    }
  }
}
