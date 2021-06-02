using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Gateway.EntityFrameworkCore
{
    public static class GatewayDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GatewayDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GatewayDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
