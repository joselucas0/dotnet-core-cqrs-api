using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SampleProject.API.Configuration
{
    internal static class SwaggerExtensions
    {
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Sample CQRS API — Clean Architecture",
                    Version = "v1",
                    Description = "A .NET Core REST API demonstrating CQRS with raw SQL (Dapper) for reads, " +
                                  "Entity Framework Core for writes, Domain-Driven Design, and Clean Architecture. " +
                                  "Features: FluentValidation, MediatR, Outbox Pattern, Cache-Aside, and structured logging with Serilog.",
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                options.IncludeXmlComments(commentsFile);
            });

            return services;
        }

        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample CQRS API — Clean Architecture V1");
            });

            return app;
        }
    }
}