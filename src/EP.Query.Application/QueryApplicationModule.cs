using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EP.Commons.ServiceApi;

namespace EP.Query
{
    [DependsOn(
        typeof(QueryCoreModule),
        typeof(EPCommonsServiceApiModule),
        typeof(AbpAutoMapperModule)
        )]
    public class QueryApplicationModule : AbpModule
    {

        public QueryApplicationModule()
        {
        }
        public override void PreInitialize()
        {
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QueryApplicationModule).GetAssembly());

        }

        public override void PostInitialize()
        {

        }
    }



}