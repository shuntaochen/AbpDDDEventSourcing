using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace EP.Query.EntityFrameworkCore
{
    [DependsOn(
        typeof(QueryCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class QueryEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QueryEntityFrameworkCoreModule).GetAssembly());
        }

        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<QueryDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        QueryDbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        QueryDbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void PostInitialize()
        {
           if (!SkipDbSeed)
            {
                //SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}