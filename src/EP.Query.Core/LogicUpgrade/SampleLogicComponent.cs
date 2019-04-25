using EP.Commons.Core.LogicalComponent;

namespace EP.Query
{
    [Logic("SampleLogic", "1.0.0", "SampleLogic")]
    public class SampleLogicComponent
    {
        [LogicField("Title", "标题", FieldType.String, "tit bi")]
        public string Title { get; set; }

    }
}
