using Abp.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Query.WebApi.Controllers
{
    public class QueryControllerBase:AbpController
    {
        protected QueryControllerBase()
        {
            LocalizationSourceName = QueryConsts.LocalizationSourceName;
        }
    }
}
