using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxService.Repositories;
using TaxService.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaxService.Services;

namespace TaxService
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddTransient<IMunicipalityTaxRepository, MunicipalityTaxRepositoryEF>();
      services.AddTransient<IEndDateService, EndDateService>();
      services.AddSingleton<MunicipalityTaxParser>();
      services.AddDbContext<MunicipalityTaxContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MunicipalityTaxContext")));

      services.AddMvc()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
          options.JsonSerializerOptions.IgnoreNullValues = true;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
