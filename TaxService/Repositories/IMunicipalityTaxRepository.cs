using System;
using TaxService.Models;

namespace TaxService.Repositories
{
  public interface IMunicipalityTaxRepository
  {
    void CreateOrUpdateMunicipalityTax(MunicipalityTax municipalityTax);
    MunicipalityTax GetMunicipalityTax(string Municipality, DateTime date);
  }
}