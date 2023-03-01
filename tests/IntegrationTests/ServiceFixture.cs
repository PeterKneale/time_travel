using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MartinCostello.Logging.XUnit;
using Microsoft.Extensions.Logging;

namespace IntegrationTests;

public class ServiceFixture : WebApplicationFactory<Invoice>, ITestOutputHelperAccessor
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x => x.AddXUnit(this));
        builder.UseSetting(Constants.Setting, "true");
        base.ConfigureWebHost(builder);
    }
    
    public ITestOutputHelper? OutputHelper { get; set; }
}