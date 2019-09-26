using System.Collections.Generic;
using TaxService.Models;
using System.IO;
using System.Linq;
using System;
using Newtonsoft.Json.Converters;
using TaxService.Services;

namespace TaxService
{
  public class MunicipalityTaxParser
  {
    private readonly IEndDateService endDateService;

    public MunicipalityTaxParser(IEndDateService endDateService)
    {
      this.endDateService = endDateService;
    }

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
      var csvValues = csvLine.Split(",").Select(v => v.Trim()).ToArray();

      //Expected format municipality,period,start date,tax
      if (csvValues.Length != 4)
        throw new FormatException("Line did not match the expected format: municipality,period,start date,tax");

      var municipalityTax = new MunicipalityTax();
      municipalityTax.Municipality = csvValues[0];
      municipalityTax.Period = ParsePeriod(csvValues[1]);
      municipalityTax.StartDate = ParseStartDate(csvValues[2]);
      municipalityTax.EndDate = endDateService.GetEndDate(municipalityTax.StartDate, municipalityTax.Period);
      municipalityTax.Tax = ParseTax(csvValues[3]);

      return municipalityTax;
    }

    private TimePeriod ParsePeriod(string periodString)
    {
      TimePeriod period;
      if (!Enum.TryParse(periodString, out period))
      {
        var acceptedValues = string.Join(",", Enum.GetValues(typeof(TimePeriod)).Cast<TimePeriod>());
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

    private double ParseTax(string taxString)
    {
      double tax;
      if (!double.TryParse(taxString, out tax))
        throw new FormatException($"Tax value has an invalid format. Value:{taxString}");

      return tax;
    }
  }
}
