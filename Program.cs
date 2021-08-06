using System;
using System.Linq;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Clients;
using Tranzact.SearchFight.Models.Interfaces;

namespace Tranzact.SearchFight
{
    class Program
    {
        static ConfigurationManager configurationManager; 
        static void Main(string[] args)
        {
            configurationManager = new ConfigurationManager();
            IRequestClient googleClient = new GoogleClient(configurationManager.GetConfiguration().SearchProviders);
            var result = googleClient.GetResultsTotal("java");
            IRequestClient bingClient = new BingClient(configurationManager.GetConfiguration().SearchProviders);
            var result2 = bingClient.GetResultsTotal("java");
            result.Wait();
            result2.Wait();
            // TODO: 2.0 Create an interface for the clients.
            // TODO: 3.0 Implement the clients.
            // TODO: 4.0 Create a Comparer class than implements the clients.
            // TODO: 4.1 Implement the clients as a list according to the existing in the config file.
            // TODO: 5.0 Create the UI display methods.
            // TODO: 6.0 Considerate some DI, in spite of it may be been useless for a console app
            // TODO: 7.0 Create a parameter parser and parameter type.
            // TODO: 8.0 Launch the comparer object from the main as an anonymous method.
            // TODO: 9.0 Check if folder structure makes sense, and refactor if needed.
        }
    }
}
