using Abp.Auditing;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EP.Commons.Core;
using EP.Query.EntityFrameworkCore;
//using EP.Query.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Configuration.Startup;
using EP.Commons.ConfigClient;
using EP.Commons.AspNetCore;
using EP.Commons.AbpMicroServiceExtensions;
using Abp.AspNetCore;
using Abp.AspNetCore.SignalR;
using Abp.AspNetCore.Configuration;
using EP.Commons.CAP;

namespace EP.Query.WebApi.Startup
{
    [DependsOn(
        typeof(EPCommonsAspNetCoreModule),
        typeof(QueryApplicationModule),
        typeof(QueryEntityFrameworkCoreModule))]
    public class QueryWebApiModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly EPCommonsAspNetCoreModule _aspNetCoreModule;
        private readonly EPCommonsAbpMicroServiceExtensionsModule _abpExtensionModule;

        public QueryWebApiModule(IConfigurationRoot configurationRoot, EPCommonsAspNetCoreModule aspNetCoreModule, EPCommonsAbpMicroServiceExtensionsModule abpExtensionModule)
        {
            _appConfiguration = configurationRoot;
            _aspNetCoreModule = aspNetCoreModule;
            _abpExtensionModule = abpExtensionModule;
        }

        public override void PreInitialize()
        {
            _aspNetCoreModule.RegisterApplicationControllers<QueryApplicationModule>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QueryWebApiModule).GetAssembly());
            _abpExtensionModule.AddMaoperProfile<QueryMappingProfile>();
        }

        public override void PostInitialize()
        {
            _abpExtensionModule.SyncPermissionDefinition<PermissionNames>();
            _abpExtensionModule.SyncLocalizationResources(typeof(QueryCoreModule).GetAssembly());
            _abpExtensionModule.SyncLogicalComponents(typeof(QueryCoreModule).GetAssembly());

        }
    }
}