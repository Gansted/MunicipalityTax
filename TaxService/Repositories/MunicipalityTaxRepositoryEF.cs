using System;
using TaxService.Models;
using TaxService.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaxService.Repositories
{
  public class MunicipalityTaxRepositoryEF : IMunicipalityTaxRepository
  {
    private readonly MunicipalityTaxContext dbContext;

    public MunicipalityTaxRepositoryEF(MunicipalityTaxContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public MunicipalityTax GetMunicipalityTax(string municipality, DateTime date)
    {
      
        var taxOnDate = dbContext.MunicipalityTaxes
          .Where(mt => mt.Municipality == municipality && mt.StartDate <= date && mt.EndDate >= date)
          .OrderBy(mt => mt.Period)
          .FirstOrDefault();

        return taxOnDate;    
    }

    public void CreateMunicipalityTax(MunicipalityTax municipalityTax)
    {
        dbContext.MunicipalityTaxes.Add(municipalityTax);
      try
      {
        dbContext.SaveChanges();
      }
      catch (DbUpdateException e)
      {
        throw new MunicipalityTaxUpdateException("Could not create tax records in database. " +
          "This is most likely due to a record already existing in the database with the same municipality, period and start date", e);
      }
    }

    public void CreateMunicipalityTaxes(IEnumerable<MunicipalityTax> municipalityTaxes)
    {
      dbContext.MunicipalityTaxes.AddRange(municipalityTaxes);
      try
      {
        dbContext.SaveChanges();
      }
      catch(DbUpdateException e)
      {
        throw new MunicipalityTaxUpdateException("Could not create tax records in database. " +
          "This is most likely due to one or more records already existing in the database with the same municipality, period and start date", e);
      }
    }

    public void UpdateMunicipalityTax(MunicipalityTax municipalityTax)
    {
        dbContext.MunicipalityTaxes.Update(municipalityTax);
        dbContext.SaveChanges();
    }
  }
}
