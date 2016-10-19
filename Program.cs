using System;

using Microsoft.Extensions.DependencyInjection;
namespace AzureSqlDotnetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {


            IServiceCollection services = new ServiceCollection();
            Startup start = new Startup(args);
            start.ConfigureServices(services);

            IServiceProvider provider = services.BuildServiceProvider();
            Console.WriteLine("Spinning up dependencies");
            DataImporter importer = provider.GetService<DataImporter>();
            Console.WriteLine("Importing data and saving to db");
            importer.ImportData();
            Console.WriteLine("Import Finished, press enter to end");
            Console.ReadLine();
        }

    }
}
