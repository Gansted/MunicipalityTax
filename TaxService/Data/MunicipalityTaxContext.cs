using Microsoft.EntityFrameworkCore;
using TaxService.Models;

namespace TaxService.Data
{
  public class MunicipalityTaxContext : DbContext
  {

    public MunicipalityTaxContext(DbContextOptions<MunicipalityTaxContext> options)
        : base(options)
    {
    }

    public DbSet<MunicipalityTax> MunicipalityTaxes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<MunicipalityTax>()
        .ToTable("MunicipalityTax")
        .HasKey(o => new { o.Municipality, o.Period, o.StartDate });
    }
  }
}
