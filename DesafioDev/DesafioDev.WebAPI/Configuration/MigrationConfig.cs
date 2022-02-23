using Microsoft.Data.SqlClient;
using Serilog;

namespace DesafioDev.WebAPI.Configuration
{
    public static class MigrationConfig
    {
        public static IServiceCollection MigrateDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var evolveConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
                var pathRoot = Directory.GetCurrentDirectory();
                var pathConfigMigration = pathRoot.Replace("DesafioDev\\DesafioDev.WebAPI", "");
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { $"{pathConfigMigration}/scripts/migrations", $"{pathConfigMigration}/scripts/dataset" },
                    IsEraseDisabled = true
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database Migration failed.", ex);
                throw;
            }

            return services;
        }
    }
}
