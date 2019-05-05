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

        public List<JObject> Query((string total, string totalCount) queryText, out Dictionary<string, string> columnDefinitions, out int totalCount, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var ret = new List<JObject>();
            columnDefinitions = new Dictionary<string, string>();
            totalCount = 0;
            MySqlCommand cmd = new MySqlCommand($"{queryText.total} limit {(pageIndex - 1) * pageSize},{pageSize}", conn);
            MySqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                totalCount = (int)new MySqlCommand(queryText.totalCount, conn).ExecuteScalar();
                columnDefinitions = GetColDefinitions(reader.GetSchemaTable());
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var line = new JObject();
                        foreach (var colDef in columnDefinitions)
                        {
                            line[colDef.Key] = reader[colDef.Key]?.ToString();

                        }
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

        private Dictionary<string, string> GetColDefinitions(DataTable schemaTable)
        {
            var ret = new Dictionary<string, string>();
            foreach (DataRow row in schemaTable.Rows)
            {
                ret.Add(row["ColumnName"].ToString(), row[11].ToString());
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


        public Dictionary<string, string> GetTableColumnDefinitions(string tableName)
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
