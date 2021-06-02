using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Gateway.Configuration;
using Gateway.Web;

namespace Gateway.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class GatewayDbContextFactory : IDesignTimeDbContextFactory<GatewayDbContext>
    {
        public GatewayDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GatewayDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            GatewayDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GatewayConsts.ConnectionStringName));
            return new GatewayDbContext(builder.Options);
        }
    }
}
