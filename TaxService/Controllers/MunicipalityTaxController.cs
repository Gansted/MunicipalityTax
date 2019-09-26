using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxService.Repositories;
using System;
using System.IO;

namespace TaxService.Controllers
{
  [Route("/[controller]")]
  public class MunicipalityTaxController : Controller
  {
    private readonly IMunicipalityTaxRepository municipalityTaxRepository;
    private readonly FileParser fileParser;

    public MunicipalityTaxController(IMunicipalityTaxRepository municipalityTaxRepository, FileParser fileParser)
    {
      this.municipalityTaxRepository = municipalityTaxRepository;
      this.fileParser = fileParser;
    }

    [HttpPost]
    public ActionResult PostTaxFile(IFormFile file)
    {
      using (var reader = new StreamReader(file.OpenReadStream()))
      {
        var municipalityTaxes = fileParser.ParseMunicipalityTaxCsv(reader);
      }
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
