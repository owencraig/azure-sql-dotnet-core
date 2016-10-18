using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
public class Startup
{
    private readonly IConfigurationRoot _config;
    public Startup(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.AddCommandLine(args)
            .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
            .AddInMemoryCollection(new Dictionary<string, string>(){
                {"xml:file", "../../products.xml"}
            })
            .AddJsonFile("app.config.json", optional: false);

        _config = builder.Build();
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();
        services.Configure<XMLReadConfig>(_config.GetSection("xml"));

        services.AddDbContext<EFContext>(o => o.UseSqlServer(_config.GetConnectionString("EFContext")));
    }

}