using Abp.Application.Navigation;
using Abp.Localization;

namespace EP.Query.WebApi.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class QueryNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, QueryConsts.LocalizationSourceName);
        }
    }
}
