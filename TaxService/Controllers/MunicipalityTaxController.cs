using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxService.Repositories;
using System;
using System.IO;
using TaxService.Models;
using TaxService.Services;

namespace TaxService.Controllers
{
  public class MunicipalityTaxController : Controller
  {
    private readonly IMunicipalityTaxRepository municipalityTaxRepository;
    private readonly MunicipalityTaxParser municipalityTaxParser;
    private readonly IEndDateService endDateService;

    public MunicipalityTaxController(IMunicipalityTaxRepository municipalityTaxRepository, MunicipalityTaxParser municipalityTaxParser, IEndDateService endDateService)
    {
      this.municipalityTaxRepository = municipalityTaxRepository;
      this.municipalityTaxParser = municipalityTaxParser;
      this.endDateService = endDateService;
    }

    [Route("AddTaxFromFile")]
    [HttpPost]
    public ActionResult AddTaxFromFile(IFormFile file)
    {
      using (var reader = new StreamReader(file.OpenReadStream()))
      {
        try
        {
          var municipalityTaxes = municipalityTaxParser.ParseMunicipalityTaxCsv(reader);
          municipalityTaxRepository.CreateMunicipalityTaxes(municipalityTaxes);
        }
        catch(FormatException e)
        {
          return BadRequest(e.Message);
        }
        catch(MunicipalityTaxUpdateException e)
        {
          return BadRequest(e.Message);
        }
      }
      return Ok();
    }

    [Route("AddTax")]
    [HttpPost]
    public ActionResult AddTax([FromBody]MunicipalityTax municipalityTax)
    {
      if (!ModelState.IsValid)
        return BadRequest("The posted model is invalid");

      municipalityTax.EndDate = endDateService.GetEndDate(municipalityTax.StartDate, municipalityTax.Period);
        try
        {
          municipalityTaxRepository.CreateMunicipalityTax(municipalityTax);
        }        
        catch (MunicipalityTaxUpdateException e)
        {
          return BadRequest(e.Message);
        }
      return Ok();
    }

    [Route("GetTax")]
    [HttpGet]
    public ActionResult GetTax(string municipality, DateTime taxDate)
    {
      var tax = municipalityTaxRepository.GetMunicipalityTax(municipality, taxDate);
      if (tax == null)
        return NotFound("No tax record was found for the given date in the given municipality");
      
      return Ok(tax.Tax);
    }
  }
}
