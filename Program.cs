using System;
using System.Linq;
using System.Threading.Tasks;
using Tranzact.SearchFight.Configuration;
using Tranzact.SearchFight.Models.Clients;
using Tranzact.SearchFight.Models.Interfaces;
using Tranzact.SearchFight.Models.Services;

namespace Tranzact.SearchFight
{
    class Program
    {
        static ConfigurationManager configurationManager; 
        static void Main(string[] args)
        {
            try
            {
                configurationManager = new ConfigurationManager();
                var task = Task.Run(() => new SearchServiceArenaService(configurationManager).Matches(args));
                task.Wait();
            }
            catch
            {
                throw;
            }
            // TODO: 8.0 Launch the comparer object from the main as an anonymous method.
            // TODO: 9.0 Check if folder structure makes sense, and refactor if needed.
            // TODO: 1.0 Improve the general error management, and add readme.txt or readme.md
        }
    }
}
