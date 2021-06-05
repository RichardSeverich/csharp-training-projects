using System.Linq;
using Autofac;
using Medium.Posts.Application;
using Medium.Posts.Application.EventBus;
using Medium.Posts.Application.IntegrationEventHandler;
using Medium.Posts.Domain.Factories;
using Medium.Posts.DomainServices;
using Medium.Posts.RabbitMQEventBus;
using Medium.Publications.Repositories.Mongo;
using Medium.Publications.Repositories.Mongo.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Medium.Posts.RestAPI
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
            // Using dotnet DI
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medium.Posts.RestAPI", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(c => new ConfigConnection(new ConfigSettings()
            {
                ConnectionString = Configuration["ConnectionSettingsMongoDB:ConnectionString"],
                DatabaseName = Configuration["ConnectionSettingsMongoDB:DatabaseName"]
            }));

            builder.RegisterAssemblyTypes(typeof(PostMongoRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(typeof(PostFactory).Assembly)
                   .Where(c => c.Name.EndsWith("Factory"))
                   .AsSelf();

            builder.RegisterAssemblyTypes(typeof(PostsService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf();

            builder.RegisterType<PostsApplication>().AsSelf();
            builder.RegisterType<PublicationPublishedIntegrationEventHandler>().AsSelf();
            builder.RegisterType<OptionalPublicationPublishedIntegrationEventHandler>().AsSelf();

            builder.Register(c => new RabbitMQEventBus.RabbitMQEventBus(
                new RabbitMqConnectionSettings(
                    Configuration["ConnectionSettingsRabbitMQ:RabbitMQHost"],
                    Configuration["ConnectionSettingsRabbitMQ:RabbitMQPort"],
                    Configuration["ConnectionSettingsRabbitMQ:RabbitMQUser"],
                    Configuration["ConnectionSettingsRabbitMQ:RabbitMQPassword"]),
                 new SubscriptionManager(),
                 c.Resolve<ILifetimeScope>()))
                .As<IEventBus>().SingleInstance();
        }

        public void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            eventBus.Subscribe<PublicationPublishedIntegrationEvent, PublicationPublishedIntegrationEventHandler>();
            eventBus.Subscribe<PublicationPublishedIntegrationEvent, OptionalPublicationPublishedIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medium.Posts.RestAPI v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }
    }
}
