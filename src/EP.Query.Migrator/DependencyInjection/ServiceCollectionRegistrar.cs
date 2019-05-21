using Microsoft.Extensions.DependencyInjection;
using Abp.Dependency;
using Microsoft.Extensions.Configuration;
using EP.Commons.Core.Configuration;
using Microsoft.Extensions.Hosting;
using EP.Commons.AbpMicroServiceExtensions;
using EP.Query.EntityFrameworkCore;
using Castle.Windsor.MsDependencyInjection;
using System.Reflection;
using EP.Commons.CAP;

namespace EP.Query.Migrator.DependencyInjection
{
    public static class ServiceCollectionRegistrar
    {
        public static void Register(IIocManager iocManager, IConfigurationRoot configuration)
        {
            IServiceCollection services = new ServiceCollection();

            var cb = AppConfigurations.GetConfigurationBuilder();

            services.AddMicroServiceExtensions<QueryDbContext>(configuration, cb);

            services.AddCAPService<QueryDbContext>(configuration);//, new Assembly[] { typeof(QueryCoreModule).Assembly });

            WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);
        }
    }
}
