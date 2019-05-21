using System;
using Abp.Dependency;
using EP.Query.EntityFrameworkCore;
using Xunit;

namespace EP.Query.Tests
{
    public class UnitTest1 : AbpTestBase
    {
        public UnitTest1(bool initializeAbp = true, IIocManager localIocManager = null) : base(initializeAbp, localIocManager)
        {
        }

        [Fact]
        public void Test1()
        {
            var x = IocManager.Instance.Resolve<QueryDbContext>();

        }
    }
}
