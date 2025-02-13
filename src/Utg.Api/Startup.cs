using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StatementIQ;
using StatementIQ.Common.Autofac;
using StatementIQ.Common.Data.DependencyInjection;
using StatementIQ.Common.Web.DependencyInjection;
using StatementIQ.Common.Web.Logging.Extensions;
using StatementIQ.Common.Web.Middleware.Extensions;
using StatementIQ.Data.Common.Migrations.Extensions;
using StatementIQ.Helpers;
using System;
using Utg.Api.Common.Handlers;
using Utg.Api.Exceptions;
using Utg.Api.Interfaces;
using Utg.Api.Services;

namespace PaceGateway.Terminal.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseCorrelationIdLogger(true);
            
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = ApiVersion.Default; 
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            services.SetupMigration(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Utg API",
                    Version = "v1",
                    Description = "Utg Web API"
                });
            });
            services.AddScoped<IUTGService, TrxService>();
            services.AddScoped<HttpPostHandler>();
            
            services.AddHttpContextAccessor();
            AssemblyHelper.LoadAssembliesToCurrentDomain();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterHttpContextAccessor();

            builder.RegisterType<UniqueIdGenerator>().WithParameter(
                (pi, c) => pi.ParameterType == typeof(long) && pi.Name == UniqueIdGenerator.WorkerIdPropertyName,
                (pi, c) => 1L).AsImplementedInterfaces().SingleInstance();
            builder.RegisterPostgresConnection(Configuration["ConnectionStrings:PgConnectionString"]);

            builder.RegisterPostgresRetry(Convert.ToInt32(Configuration["Retry:Count"]),
                Convert.ToInt32(Configuration["Retry:Delay"]));

            builder.RegisterPostgresUnitOfWork();
            AssemblyHelper.LoadAssembliesToCurrentDomain();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterByAttributes(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCorrelationIdMiddleware();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(Convert.ToInt32(Configuration["WebSocketSettings:KeepAliveInterval"])),
            };
           // webSocketOptions.AllowedOrigins.Add(Configuration["WebSocketSettings:AllowedOrigin"]);
            app.UseWebSockets(webSocketOptions);
            if (Configuration.GetValue<string>("AppSettings:ApplicationConfig:CaptureRequestResponseLogging") == "True")
            {
                app.UseRequestResponseLogging();
            }
            app.UseSecurityHeaderMiddleware();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>(Configuration["service"]);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "UTG API"); });
        }
    }
}