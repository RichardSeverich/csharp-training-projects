namespace APIGateway
{

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore;
    using Microsoft.Extensions.Configuration;

    public class Program
    {

         public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((host, config) =>
           {
               config.AddJsonFile("configuration.json");
           })
               .UseStartup<Startup>();
    }
}
