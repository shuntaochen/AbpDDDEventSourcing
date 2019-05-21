using Abp.Modules;
using EP.Commons.AspNetCore;
using EP.Query.EntityFrameworkCore;
using EP.Commons.Core;

namespace EP.Query.Tests
{
    [DependsOn(
    typeof(EPCommonsCoreModule),
    typeof(QueryApplicationModule),
    typeof(QueryEntityFrameworkCoreModule))]
    public class TestModule : AbpModule
    {

    }
}
