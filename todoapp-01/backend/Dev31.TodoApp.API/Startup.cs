// <copyright file="Startup.cs">
//    Copyright (c) jala.
// </copyright>
namespace Dev31.TodoApp.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

    using Dev31.TodoApp.Data.Contexts;
    using Dev31.TodoApp.Data.Repositories;
    using Dev31.TodoApp.Interfaces.Repositories;
    using Dev31.TodoApp.Interfaces.Services;
    using Dev31.TodoApp.Logic.Communication;
    using Dev31.TodoApp.Logic.Services;
    using Dev31.TodoApp.Models;
    using Dev31.TodoApp.Utilities;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Class Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// builder
        /// </summary>
        /// <see cref="Startup"/>
        /// <param name="configuration">configuration to the application</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Constants.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options => options.AddPolicy("TodoAppPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("AppContextConnectionString")));

            services.AddScoped<IRepository<TodoTask, PostOptions, Guid>, TaskRepository>();
            services.AddScoped<IRepository<Tag, PostOptions, string>, TagRepository>();
            services.AddScoped<IRepository<TaskTag, PostOptions, Guid>, TaskTagRepository>();
            services.AddScoped<IRepository<Project, PostOptions, Guid>, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService<TodoAppAPIResponse<User>, TodoAppAPIResponse<UserAuthenticated>>, UserService >();
            services.AddScoped<IProjectService<TodoAppAPIResponse<Project>>, ProjectService>();
            services.AddScoped<ITagService<TodoAppAPIResponse<Tag>>, TagService>();
            services.AddScoped<ITaskService<TodoAppAPIResponse<TodoTask>>, TaskService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// property Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
    }
}
