using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using EP.Query.DataSource;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace EP.Query.EntityFrameworkCore
{
    public class QueryDbContext : AbpDbContext
    {

        public static readonly LoggerFactory LoggerFactory =
       new LoggerFactory(new[] { new DebugLoggerProvider((_, __) => true) });



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }


        //Add DbSet properties for your entities...

        public QueryDbContext(DbContextOptions<QueryDbContext> options)
            : base(options)
        {
        }

        public DbSet<DataSource.DataSource> DataSources { get; set; }
        public DbSet<DataSourceField> DataSourceFields { get; set; }
        public DbSet<DataSourceFolder> DataSourceFolders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<JObject>();
            // 自定义必须包括 base.OnModelCreating(builder)
            base.OnModelCreating(builder);


            // 添加更多自定义设置
            //builder.ApplyConfiguration(new SimpleEntityConfig());

            //自动引用EntityConfigs的配置
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
