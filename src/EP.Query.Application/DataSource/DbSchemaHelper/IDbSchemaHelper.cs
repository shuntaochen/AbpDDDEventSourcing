using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace EP.Query.DataSource
{
    public interface IDbSchemaHelper : IDisposable
    {
        void Dispose();
        List<string> GetTableNames();
        Dictionary<string, string> GetTableColumnDefinitions(string tableName);
        List<JObject> Query((string total, string totalCount) queryText, out Dictionary<string, string> columnDefinitions, out int totalCount, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}