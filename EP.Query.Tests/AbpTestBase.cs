using Abp.Dependency;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using Effort;
using EP.Commons.Core.Configuration;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace EP.Query.Tests
{
    public class AbpTestBase : AbpIntegratedTestBase<TestModule>
    {
        public AbpTestBase(bool initializeAbp = true, IIocManager localIocManager = null) : base(initializeAbp, localIocManager)
        {

        }

        protected override void PreInitialize()
        {

            base.PreInitialize();

            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );

            LocalIocManager.IocContainer.Register(Component.For<IOptionsSnapshot<CustomConfigSection>>().ImplementedBy<MyOption>().LifestyleTransient());

        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
