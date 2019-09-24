using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxService.Repositories;
using System;


namespace TaxService.Controllers
{
  [Route("/[controller]")]
  public class MunicipalityTaxController : Controller
  {
    private readonly IMunicipalityTaxRepository municipalityTaxRepository;

    public MunicipalityTaxController(IMunicipalityTaxRepository municipalityTaxRepository)
    {
      this.municipalityTaxRepository = municipalityTaxRepository;
    }

    [HttpPost]
    public ActionResult PostTaxFile(IFormFile file)
    {
      return null;
    }

    [HttpGet]
    public ActionResult GetTax(string municipality, DateTime date)
    {
      var tax = municipalityTaxRepository.GetMunicipalityTax(municipality, date);
      return Ok(tax);
    }
  }
}
