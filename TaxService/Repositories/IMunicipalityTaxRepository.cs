using System;
using TaxService.Models;

namespace TaxService.Repositories
{
  public interface IMunicipalityTaxRepository
  {
    void UpdateMunicipalityTax(MunicipalityTax municipalityTax);
    void CreateMunicipalityTax(MunicipalityTax municipalityTax);
    MunicipalityTax GetMunicipalityTax(string Municipality, DateTime date);
  }
}