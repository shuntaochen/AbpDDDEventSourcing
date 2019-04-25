using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;
using EP.Query.DataSource;
using EP.Query.DataSource.EventHandlers;
using EP.Query.Localization;

namespace EP.Query
{
    public class QueryCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            QueryLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QueryCoreModule).GetAssembly());
        }
    }


}