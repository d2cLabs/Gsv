using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Gsv.EntityFrameworkCore
{
    public static class GsvDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GsvDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GsvDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}