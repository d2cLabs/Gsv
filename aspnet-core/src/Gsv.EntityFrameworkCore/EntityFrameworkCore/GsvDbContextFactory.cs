using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Gsv.Configuration;
using Gsv.Web;

namespace Gsv.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class GsvDbContextFactory : IDesignTimeDbContextFactory<GsvDbContext>
    {
        public GsvDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GsvDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            GsvDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GsvConsts.ConnectionStringName));

            return new GsvDbContext(builder.Options);
        }
    }
}
