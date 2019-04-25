using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace EP.Query.DataSource
{
    public class MysqlSchemaHelper : IDbSchemaHelper
    {
        private readonly MySqlConnection conn;

        public MysqlSchemaHelper(MySqlConnection conn)
        {
            this.conn = conn;
            conn.Open();
        }

        public string CreateView(string query)
        {
            var tempView = Guid.NewGuid().ToString();
            var view = $"create view {tempView} as {query}";
            MySqlCommand cmd = new MySqlCommand(view, conn);
            cmd.ExecuteNonQuery();
            return tempView;
        }

        public List<JObject> QueryView(string view)
        {
            var ret = new List<JObject>();
            var sql = $"select * from {view}";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                var colNames = GetColNames(reader.GetSchemaTable());
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string val = reader.GetString(0);
                        var line = new JObject();
                        colNames.ForEach(colName =>
                        {
                            line[colName] = reader[colName]?.ToString();
                        });
                        ret.Add(line);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                reader.Close();
            }
            return ret;
        }

        private List<string> GetColNames(DataTable schemaTable)
        {
            var ret = new List<string>();
            foreach (DataRow row in schemaTable.Rows)
            {
                ret.Add(row["ColumnName"].ToString());
            }
            return ret;
        }

        public List<string> GetTableNames()
        {
            var list_tblName = new List<string>();
            string sql = "show tables;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string t = reader.GetString(0);
                        list_tblName.Add(t);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                reader.Close();
            }
            return list_tblName;
        }


        public Dictionary<string, string> GetColumnDefinitions(string tableName)
        {
            Dictionary<string, string> fieldDef = new Dictionary<string, string>();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            List<string> list_ColName = new List<string>();
            List<Type> list_ColType = new List<Type>();
            string sql = "show columns from " + tableName + " ;";
            cmd = new MySqlCommand(sql, conn);
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string t = reader.GetString(0);
                        Type tt = reader.GetValue(1) as Type;

                        string ttt = reader.GetString(1);
                        fieldDef.Add(t, ttt.ToSysPreDefined());
                    }
                }
                reader.Close();
            }
            catch (Exception e) { }
            return fieldDef;
        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}
