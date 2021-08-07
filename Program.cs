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
            try
            {
                var task = Task.Run(() =>
                    new SearchServiceArenaService(CreateServiceClients(CreateConfigurationmanager()), CreateDisplayService())
                        .Matches(args)
                    );
                task.Wait();
            }
            catch
            {
                throw;
            }
            // TODO: 9.0 Check if folder structure makes sense, and refactor if needed.
            // TODO: 10.0 Improve the general error management, and add readme.txt or readme.md
            // TODO: 11 add unit test and coverage for main components.
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
