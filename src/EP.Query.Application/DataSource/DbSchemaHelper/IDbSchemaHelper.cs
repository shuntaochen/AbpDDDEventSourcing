using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace EP.Query.DataSource
{
    public interface IDbSchemaHelper : IDisposable
    {
        string CreateView(string query);
        void Dispose();
        Dictionary<string, string> GetColumnDefinitions(string tableName);
        List<string> GetTableNames();
        List<JObject> QueryView(string view);
    }
}