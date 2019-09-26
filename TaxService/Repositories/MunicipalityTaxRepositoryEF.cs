using System;
using TaxService.Models;
using TaxService.Data;
using System.Linq;

namespace TaxService.Repositories
{
  public class MunicipalityTaxRepositoryEF : IMunicipalityTaxRepository
  {
    public MunicipalityTax GetMunicipalityTax(string municipality, DateTime date)
    {
      using (var dbContext = new MunicipalityTaxContext())
      {
        var taxOnDate = dbContext.MunicipalityTaxes
          .Where(mt => mt.Municipality == municipality && mt.StartDate >= date && mt.EndDate <= date)
          .OrderBy(mt => mt.Period)
          .FirstOrDefault();

        return taxOnDate;
      }      
    }

    public void CreateMunicipalityTax(MunicipalityTax municipalityTax)
    {
      using (var dbContext = new MunicipalityTaxContext())
      {
        dbContext.MunicipalityTaxes.Add(municipalityTax);
        dbContext.SaveChanges();
      }
    }

    public void UpdateMunicipalityTax(MunicipalityTax municipalityTax)
    {
      using (var dbContext = new MunicipalityTaxContext())
      {
        dbContext.MunicipalityTaxes.Update(municipalityTax);
        dbContext.SaveChanges();
      }
    }
  }
}
