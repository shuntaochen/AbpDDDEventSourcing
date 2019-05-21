using EP.Commons.Core.Configuration;
using Microsoft.Extensions.Options;

namespace EP.Query.Tests
{
    public class MyOption : IOptionsSnapshot<CustomConfigSection>
    {
        public CustomConfigSection Value => new CustomConfigSection();

        public CustomConfigSection Get(string name)
        {
            return Value;
        }
    }
}
