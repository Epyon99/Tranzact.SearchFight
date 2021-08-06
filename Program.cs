using System;
using System.Linq;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Clients;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.Service;

namespace Tranzact.SearchFight
{
    class Program
    {
        static ConfigurationManager configurationManager; 
        static void Main(string[] args)
        {
            configurationManager = new ConfigurationManager();
            SearchComparerService scs = new SearchComparerService(configurationManager);
            string[] param = { "java", "net", "python" };
            scs.Matches(param);
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
