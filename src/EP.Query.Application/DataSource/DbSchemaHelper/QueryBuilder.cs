using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.Query.DataSource
{
    public class QueryBuilder
    {
        public QueryBuilder()
        {
        }


        private string _tableName;
        private List<string> _conditions;
        public void AddTableName(string tableName)
        {
            _tableName = tableName;
        }

        public string Build()
        {
            return $"select * from {_tableName} where 1=1 and {string.Join(" and ", _conditions.Select(c => c.Contains('=') ? $"{c.Split('=')[0]}='{c.Split('=')[1]}'" : c))}";
        }

        public void AddAndConditions(List<string> andConditions)
        {
            _conditions = andConditions;
        }
    }
}