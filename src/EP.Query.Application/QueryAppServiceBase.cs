using Abp.Application.Services;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Runtime.Session;
using DotNetCore.CAP;
using System.Globalization;
using System.Linq;
//using EP.Commons.ConfigClient;

namespace EP.Query
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class QueryAppServiceBase : ApplicationService
    {
        protected QueryAppServiceBase()
        {
            LocalizationSourceName = QueryConsts.LocalizationSourceName;
        }


        protected string L(string name)
        {
            return L(name, new CultureInfo("zh-Hans"));
        }

    }
}