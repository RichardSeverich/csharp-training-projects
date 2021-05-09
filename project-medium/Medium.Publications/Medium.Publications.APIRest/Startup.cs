using Autofac;
using Medium.Publications.Application;
using Medium.Publications.Domain.Factories;
using Medium.Publications.Repositories.Mongo;
using Medium.Publications.Repositories.Mongo.Config;
using Medium.Publications.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Medium.Publications.APIRest
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medium.Publications.APIRest", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.Register(c => new ConfigConnection(new ConfigSettings()
            {
                ConnectionString = "mongodb://root:root@localhost:27017",
                DatabaseName = "medium_publications"
            }));

            builder.RegisterAssemblyTypes(typeof(PublicationMongoRepository).Assembly)
                    .Where(c => c.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(PublicationFactory).Assembly)
                    .Where(c => c.Name.EndsWith("Factory"))
                    .AsSelf();

            builder.RegisterAssemblyTypes(typeof(PublicationServices).Assembly)
                    .Where(c => c.Name.EndsWith("Services"))
                    .AsSelf();

            builder.RegisterAssemblyTypes(typeof(App).Assembly)
                    .Where(c => c.Name.EndsWith("App"))
                    .AsSelf();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medium.Publications.APIRest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
