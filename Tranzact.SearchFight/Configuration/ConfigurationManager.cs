using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Tranzact.SearchFight.Common.Exceptions;

namespace Tranzact.SearchFight.Configuration
{
    public class ConfigurationManager
    {
        public const string ConfigurationFileName = "appsettings.json";
        private readonly Configuration Config;

        public ConfigurationManager(string filepath)
        {
            if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
            {
                Config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(filepath));
            }
        }

        public Configuration GetConfiguration()
        {
            if (Config == null || Config.EnabledSearchProviders == null)
            {
                throw new NoConfigurationFileException("No configuration found");
            }
            if (Config.EnabledSearchProviders.Distinct().Count() < 2)
            {
                throw new LessThanTwoEnginesException("You need at least 2 search providers in order to use the application");
            }
            return Config;
        }
    }
}
