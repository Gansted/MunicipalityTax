using System;
using System.Collections.Generic;
using TaxService.Models;

namespace TaxService.Repositories
{
  public interface IMunicipalityTaxRepository
  {
    void UpdateMunicipalityTax(MunicipalityTax municipalityTax);
    void CreateMunicipalityTax(MunicipalityTax municipalityTax);
    void CreateMunicipalityTaxes(IEnumerable<MunicipalityTax> municipalityTax);
    MunicipalityTax GetMunicipalityTax(string Municipality, DateTime date);
  }
}