using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EP.Query.EntityFrameworkCore
{
    public static class QueryDbContextOptionsConfigurer
    {
        //public static void Configure(
        //    DbContextOptionsBuilder<QueryDbContext> dbContextOptions, 
        //    string connectionString
        //    )
        //{
        //    /* This is the single point to configure DbContextOptions for QueryDbContext */
        //    dbContextOptions.UseSqlServer(connectionString);
        //}

        public static void Configure(DbContextOptionsBuilder<QueryDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<QueryDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
