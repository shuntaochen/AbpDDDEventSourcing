//using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
//using EP.Commons.ConfigClient;
using System.IO;
//using static EP.Commons.ConfigClient.ConsulConfigurationExtensions;
//using EP.Commons.Rest;
//using EP.Commons.Rest.Extension;
using Abp.Threading;
using System.Text;
using Newtonsoft.Json.Linq;
using EP.Query.Localization;
using System.Threading.Tasks;
using EP.Commons.ServiceApi;
using EP.Commons.ServiceApi.UserCenter;
using EP.Commons.Core;
using Microsoft.Extensions.Options;
using EP.Commons.Core.Configuration;
using Abp.AutoMapper;
using EP.Commons.ServiceApi.DynamicForms;
using EP.Commons.ServiceApi.DynamicForms.Dto;
using EP.Commons.Core.LogicalComponent;
using System.Collections.Generic;
using System;
using Abp.Dependency;
using System.Linq;

namespace EP.Query
{
    [DependsOn(
        typeof(QueryCoreModule),
        typeof(EPCommonsServiceApiModule),
        typeof(AbpAutoMapperModule)
        )]
    public class QueryApplicationModule : AbpModule
    {
        private readonly CustomConfigSection configurationOption;

        public QueryApplicationModule(IOptionsSnapshot<CustomConfigSection> configurationOption)
        {
            this.configurationOption = configurationOption.Value;
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