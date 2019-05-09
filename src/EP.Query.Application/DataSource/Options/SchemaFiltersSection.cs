using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
namespace EP.Query.DataSource.Options
{

    public class SchemaFiltersSection
    {

        public List<string> HiddenTables { get; set; } = new List<string>();

        public List<string> HiddenColumns { get; set; } = new List<string>();

        public SchemaFiltersSection()
        {
            HiddenTables.AddRange(new string[] { "cap.published", "cap.received", "__EFMigrationsHistory" });
        }


    }

}