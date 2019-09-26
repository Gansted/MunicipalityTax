using System;
using TaxService.Models;

namespace TaxService.Services
{
  public interface IEndDateService
  {
    DateTime GetEndDate(DateTime startDate, TimePeriod period);
  }
}