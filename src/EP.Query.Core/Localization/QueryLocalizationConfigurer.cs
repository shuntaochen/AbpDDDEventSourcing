using System.IO;
using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Reflection.Extensions;

namespace EP.Query.Localization
{
    public static class QueryLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flags england", isDefault: true));
            localizationConfiguration.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flags tr"));
            localizationConfiguration.Languages.Add(new LanguageInfo("zh-Hans", "Chinese", "中文 中国"));


            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(QueryConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(QueryLocalizationConfigurer).GetAssembly(),
                        "EP.Query.Localize.SourceFiles"
                    )
                )
            );



        }

    }
}