using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranzact.SearchFight.Clients;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.Services;
using Tranzact.SearchFight.Services;

namespace Tranzact.SearchFight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var task = Task.Run(() =>
                new SearchServiceArenaService(CreateServiceClients(CreateConfigurationmanager()), CreateDisplayService())
                    .Matches(args)
                );
            task.Wait();
            // 10.1 Write the details in the readme
            // 11.1 Cleanup UnitTest and abstraction, and remove duplicates.
            // 12.0 Review syntax and code coverage manually
        }

        private static List<IWebClient> CreateServiceClients(ConfigurationManager configurationManager)
        {
            var requestClients = new List<IWebClient>();
            var config = configurationManager.GetConfiguration();
            if (config.EnabledSearchProviders.Any(g => g == BingClient.SearchProviderName))
            {
                requestClients.Add(new BingClient(config.BingSearchEngine, CreateHttpClient(config.BingSearchEngine.URI)));
            }
            if (config.EnabledSearchProviders.Any(g => g == GoogleClient.SearchProviderName))
            {
                requestClients.Add(new GoogleClient(config.GoogleSearchEngine, CreateHttpClient(config.GoogleSearchEngine.URI)));
            }
            return requestClients;
        }

        private static ConfigurationManager CreateConfigurationmanager()
        {
            return new ConfigurationManager(ConfigurationManager.ConfigurationFileName);
        }

        private static DisplayService CreateDisplayService()
        {
            return new DisplayService();
        }

        private static IHttpClient CreateHttpClient(string url)
        {
            return new BaseHttpClient(url);
        }


    }
}
