using System;

namespace TaxService.Models
{
  public class MunicipalityTax
  {
    public DateTime StartDate { get; set; }
    public string Municipality { get; set; }
    public TimePeriod Period { get; set; }
    public double Tax { get; set; }
  }
}
