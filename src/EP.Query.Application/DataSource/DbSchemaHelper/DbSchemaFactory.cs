using Abp.Dependency;
using EP.Query.DataSource.Options;
using EP.Query.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;

namespace EP.Query.DataSource
{
    public class DbSchemaFactory : ITransientDependency
    {
        private readonly SchemaFiltersSection filterConfig;

        public DbSchemaFactory(QueryDbContext queryDbContext,IOptions<SchemaFiltersSection> filterConfig)
        {
            QueryDbContext = queryDbContext;
            this.filterConfig = filterConfig.Value;
        }

        public QueryDbContext QueryDbContext { get; }

        public IDbSchemaHelper Create()
        {
            return new MysqlSchemaHelper(QueryDbContext.Database.GetDbConnection() as MySqlConnection,filterConfig);
        }
    }
}