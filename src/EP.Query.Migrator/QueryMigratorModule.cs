using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EP.Commons.AbpMicroServiceExtensions;
using EP.Commons.Migrator;
using EP.Commons.Core.Configuration;
using Microsoft.Extensions.Hosting;
using EP.Commons.Consul;
using EP.Query.EntityFrameworkCore;
using EP.Commons.Core;

namespace EP.Query.Migrator
{
    [DependsOn(typeof(QueryEntityFrameworkCoreModule)
        , typeof(EPCommonsAbpMicroServiceExtensionsModule)
        , typeof(EPCommonsMigratorModule))]
    public class QueryMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public QueryMigratorModule(QueryEntityFrameworkCoreModule abpProjectNameEntityFrameworkModule, IConfigurationRoot appConfiguration)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = appConfiguration;
        }

        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus),
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QueryMigratorModule).GetAssembly());
        }
    }
}
