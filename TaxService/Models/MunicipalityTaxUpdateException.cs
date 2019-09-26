using System;

namespace TaxService.Models
{
  public class MunicipalityTaxUpdateException : Exception
  {
    public MunicipalityTaxUpdateException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
