using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;



using System.IO;

namespace Baristasyon.Persistence.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BaristasyonDbContext>
    {
        public BaristasyonDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "Baristasyon.API");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath) // 🔧 migration config'i API projesinden alacak
                .AddJsonFile("appsettings.json", optional: false)
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<BaristasyonDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new BaristasyonDbContext(optionsBuilder.Options);
        }
    }
}
