using System;
using TaxService.Models;

namespace TaxService.Repositories
{
  public class MunicipalityTaxRepository : IMunicipalityTaxRepository
  {
    public MunicipalityTax GetMunicipalityTax(string municipality, DateTime date)
    {
      return new MunicipalityTax { Municipality = municipality, StartDate = date };
    }

    public void CreateOrUpdateMunicipalityTax(MunicipalityTax municipalityTax)
    {

    }
  }
}
