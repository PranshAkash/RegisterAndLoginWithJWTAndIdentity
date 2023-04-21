using Data.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Helper.Migrations
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("LoanInvestor");
                    migrationService.ListMigrations();
                    migrationService.MigrateUp(202106280001);
                    migrationService.MigrateUp(202106280003);
                    migrationService.MigrateUp(202106280002);
                    //migrationService.MigrateDown(202106280001); //To revert specific migraton
                }
                catch (Exception ex)
                {
                    //log errors or ...
                    throw;
                }
            }
            return host;
        }

        public static string MigrateDatabase(IServiceProvider ServiceProvider, string DatabaseName)
        {
            string result = "Migration not started";
            try
            {
                //AppConfig.AppConfigurationJson(DatabaseName);
                var databaseService = ServiceProvider.GetRequiredService<Database>();
                var migrationService = ServiceProvider.GetRequiredService<IMigrationRunner>();
                databaseService.CreateDatabase(DatabaseName);
                migrationService.ListMigrations();
                migrationService.MigrateUp();
                //migrationService.MigrateDown(202106280001); //To revert specific migraton
                result = "Migration completed";
            }
            catch (Exception ex)
            {
                //log errors or ...
                result = ex.Message;
                throw;
            }
            return result;
        }
    }
}
