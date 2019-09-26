using System.Collections.Generic;
using TaxService.Models;
using System.IO;
using System.Linq;
using System;

namespace TaxService
{
  public class FileParser
  {
    public IEnumerable<MunicipalityTax> ParseMunicipalityTaxCsv(TextReader reader)
    {
      var municipalityTaxes = new List<MunicipalityTax>();

      while (reader.Peek() >= 0)
      {
        municipalityTaxes.Add(ParseMunicipalityTax(reader.ReadLine()));
      }
      return municipalityTaxes;
    }

    private MunicipalityTax ParseMunicipalityTax(string csvLine)
    {
      var csvValues = csvLine.Split().Select(v => v.Trim()).ToArray();

      //Expected format municipality,period,start date,tax
      if (csvValues.Length != 4)
        throw new FormatException("Line did not match the expected format: municipality,period,start date,tax");

      var municipalityTax = new MunicipalityTax();
      municipalityTax.Municipality = csvValues[0];
      municipalityTax.Period = ParsePeriod(csvValues[1]);
      municipalityTax.StartDate = ParseStartDate(csvValues[2]);
      municipalityTax.EndDate = GetEndDate(municipalityTax.StartDate, municipalityTax.Period);
      municipalityTax.Tax = ParseTax(csvValues[3]);

      return municipalityTax;
    }

    private TimePeriod ParsePeriod(string periodString)
    {
      TimePeriod period;
      if (!Enum.TryParse(periodString, out period))
      {
        var acceptedValues = string.Join(",", Enum.GetValues(typeof(TimePeriod)));
        throw new FormatException($"Period value has an invalid format. Accepted values: {acceptedValues}. Actual value:{periodString}");
      }

      return period;
    }

    private DateTime ParseStartDate(string startDateString)
    {
      DateTime startDate;
      if (!DateTime.TryParse(startDateString, out startDate))
        throw new FormatException($"Start date value has an invalid format. Value:{startDateString}");

      return startDate;
    }

    //Users do not enter end dates, these are calculated by start date and time period to ensure consistensy between period and dates.
    private DateTime GetEndDate(DateTime startDate, TimePeriod period)
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

    private double ParseTax(string taxString)
    {
      double tax;
      if (!double.TryParse(taxString, out tax))
        throw new FormatException($"Tax value has an invalid format. Value:{taxString}");

      return tax;
    }
  }
}
