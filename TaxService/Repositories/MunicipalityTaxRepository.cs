using System;
using TaxService.Models;

namespace TaxService.Repositories
{
  public class MunicipalityTaxRepository : IMunicipalityTaxRepository
  {
    public MunicipalityTax GetMunicipalityTax(string Municipality, DateTime date)
    {
      return new MunicipalityTax();
    }

    public void CreateOrUpdateMunicipalityTax(MunicipalityTax municipalityTax)
    {

    }
  }
}
