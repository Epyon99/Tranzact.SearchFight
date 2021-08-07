using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Tranzact.SearchFight.Configuration
{
    public class ConfigurationManager
    {
        public const string ConfigurationFileName = "appsettings.json";
        private readonly Configuration Config;

        public ConfigurationManager()
        {
            if (File.Exists(ConfigurationFileName))
            {
                Config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(ConfigurationFileName));
            }
        }

        public Configuration GetConfiguration()
        {
            if (Config == null || Config.EnabledSearchProviders == null)
            {
                throw new Exception("No configuration found");
            }
            if (Config.EnabledSearchProviders.Distinct().Count() < 2)
            {
                throw new Exception("You need at least 2 search providers in order to use the application");
            }
            return Config;
        }
    }
}
