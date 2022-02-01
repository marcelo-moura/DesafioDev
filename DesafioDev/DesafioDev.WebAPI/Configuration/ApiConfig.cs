using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddConfigurationApi(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseConfigurationApi(this IApplicationBuilder app)
        {
            app.UseCors("Development");
            return app;
        }
    }
}
