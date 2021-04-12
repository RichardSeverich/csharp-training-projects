// <copyright file="Program.cs">
//  Todoapp
// </copyright>
namespace Dev31.TodoApp.API
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {

        /// <summary>
        /// Our main entry point
        /// </summary>
        /// <param name="args">Arguments to be passed to the application</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// create host builder
        /// </summary>
        /// <param name="args">Arguments to be passed to the application</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
