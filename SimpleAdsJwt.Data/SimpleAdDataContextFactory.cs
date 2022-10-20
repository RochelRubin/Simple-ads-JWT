using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SimpleAdsJwt.Data
{
    public class SimpleAdDataContextFactory : IDesignTimeDbContextFactory<SimpleAdDataContext>
    {
        public SimpleAdDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}SimpleAdsJwt.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new SimpleAdDataContext(config.GetConnectionString("ConStr"));
        }
    }
}
