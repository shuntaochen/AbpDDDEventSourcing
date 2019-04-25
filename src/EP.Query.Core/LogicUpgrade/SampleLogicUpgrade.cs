using EP.Commons.Migrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Query.LogicUpgrade
{
    public class SampleLogicUpgrade : ILogicUpgrade
    {
        public string Name { get; set; } = "SampleLogic";
        public string Version { get; set; } = "1.0.1";

        public void Update()
        {
        }
    }
}
