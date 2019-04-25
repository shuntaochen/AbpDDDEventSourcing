using EP.Commons.Core.Configuration;
using EP.Commons.Core.Extensions;
using EP.Commons.Core.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EP.Query.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class QueryDbContextFactory : IDesignTimeDbContextFactory<QueryDbContext>
    {
        public QueryDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<QueryDbContext>();
            var configuration = AppConfigurations.GetConfiguration(WebContentDirectoryFinder.CalculateContentRootFolder());
            var configOptions = configuration.GetConfigSection();
            QueryDbContextOptionsConfigurer.Configure(
                builder,
                configuration[configOptions.Db.DefaultConnectionStringName]
            );

            return new QueryDbContext(builder.Options);
        }
    }
}