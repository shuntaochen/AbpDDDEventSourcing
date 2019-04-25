using Abp.Dependency;
using EP.Query.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;

namespace EP.Query.DataSource
{
    public class DbSchemaFactory : ITransientDependency
    {
        public DbSchemaFactory(QueryDbContext queryDbContext)
        {
            QueryDbContext = queryDbContext;
        }

        public QueryDbContext QueryDbContext { get; }

        public IDbSchemaHelper Create()
        {
            return new MysqlSchemaHelper(QueryDbContext.Database.GetDbConnection() as MySqlConnection);
        }
    }
}